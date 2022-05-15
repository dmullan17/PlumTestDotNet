using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PlumTest
{
    public class Worker
    {

	    protected IActions _actions;

        private List<string> Providers = new List<string>() { "bit.ly", "tinyurl.com" };
	    private string providerToUse;
	    private string url;

        //public Models.Response GetShortLink(Models.Request request)
        //{
        //    throw new NotImplementedException();
        //}

        public Worker(IActions actions)
        {
	        _actions = actions;
        }

        public async void Run()
        {
	        Models.Request request = GetUserInput();
	        var response = await GetShortLinkAsync(request);
        }

        private Models.Request GetUserInput()
        {

	        Console.WriteLine("Enter URL to shorten: ");

	        string unvalidatedUrl = Console.ReadLine();


            if (!ValidateUrl(unvalidatedUrl)) {
		        Environment.Exit(0);
	        }

	        url = unvalidatedUrl;

            Console.WriteLine("What provider should be used?");

	        for (var i = 0; i < Providers.Count; i++)
	        {
		        Console.WriteLine($"Type {i + 1} for {Providers[i]}");
            }

	        int providerInt;
	        bool success = int.TryParse(Console.ReadLine(), out providerInt);

	        if (success) {
		        if (!ValidateProviderInt(providerInt)) {
			        Environment.Exit(0);
		        }
	        } else {
		        Environment.Exit(0);
	        }

	        providerToUse = Providers[providerInt - 1];

            Models.Request request = new Models.Request {
                url = url,
                provider = providerToUse
            };

            return request;

        }

        private bool ValidateUrl(string url) {
	        if (url == null || !url.Contains(".")) {
		        Console.WriteLine("URL invalid");
		        return false;
	        }

	        return true;
        }

        private bool ValidateProviderInt(int provider) {

	        if (!provider.Equals(1) && !provider.Equals(2)) {
		        Console.WriteLine("Invalid provider");
		        return false;
	        }

	        return true;
        }

        public async Task<Models.Response> GetShortLinkAsync(Models.Request request)
        {
	        var response = await _actions.GetAsync(request);

	        return new Models.Response();
        }
    }
}
