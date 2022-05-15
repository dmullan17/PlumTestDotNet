using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PlumTest {
    public sealed class Actions : IActions {

	    private static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(15);

	    private readonly IDictionary<string, string> _headers;

	    public Actions()
	    {
		    _headers = new Dictionary<string, string>();
	    }

	    public async Task<HttpResponseMessage> GetAsync(Models.Request request)
	    {
		    //using (var client = new HttpClient { BaseAddress = new Uri(string.Format("https://{0}/", request.provider)) }) {
		    using (var client = new HttpClient()) {
			    client.DefaultRequestHeaders.Clear();
			    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			    client.Timeout = DefaultTimeout;

			    foreach (var kvp in _headers) {
				    client.DefaultRequestHeaders.Add(kvp.Key, kvp.Value);
			    }

			    HttpResponseMessage response;

			    var body = "";

			    if (request.provider == "bit.ly")
			    {
				    body = "";
			    } 
			    else if (request.provider == "tinyurl.com")
			    {
				    body = string.Format("http://{0}/api-create.php?url={1}", request.provider, request.url);
			    }

			    try {
				    response = await client.GetAsync(body).ConfigureAwait(false);
			    } catch (HttpRequestException) {
				    return new HttpResponseMessage(HttpStatusCode.InternalServerError);
			    }

			    return response;
		    }
        }
    }
}
