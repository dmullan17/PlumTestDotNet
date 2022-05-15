using System.Net.Http;
using System.Threading.Tasks;

namespace PlumTest
{
	public interface IActions
    {
        //public Models.Response GetShortLink(Models.Request request);
        public Task<HttpResponseMessage> GetAsync(Models.Request request);
    }
}
