using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Serilog;
using System;
using System.ComponentModel;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace PlumTest
{
    class Program
    {

	    static void Main(string[] args)
        {

	        //Worker worker = new Worker();
         //   worker.Run();

            var builder = new ConfigurationBuilder();
            BuildConfig(builder);

            Log.Logger = new LoggerConfiguration()
	            .ReadFrom.Configuration(builder.Build())
	            .Enrich.FromLogContext()
	            .WriteTo.Console()
	            .CreateLogger();

            Log.Logger.Information("Application Starting");

            var host = Host.CreateDefaultBuilder()
	            .ConfigureServices((context, services) => {
		            services.AddTransient<IActions, Actions>();
	            })
	            .UseSerilog()
	            .Build();

            var svc = ActivatorUtilities.CreateInstance<Worker>(host.Services);
            svc.Run();

        }

        static void BuildConfig(IConfigurationBuilder builder) {
	        builder.SetBasePath(Directory.GetCurrentDirectory())
		        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
		        .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
		        .AddEnvironmentVariables();
        }


        public static bool ValidateUrl(string url)
        {
            if (url == null || !url.Contains("."))
            {
                Console.WriteLine("URL invalid");
                return false;
            }

            return true;
        }

        public static bool ValidateProviderInt(int provider)
        {
            
            if (!provider.Equals(1) && !provider.Equals(2))
            {
                Console.WriteLine("Invalid provider");
                return false;
            }

            return true;
        }

        public Models.Response GetShortLink(Models.Request request)
        {
            throw new NotImplementedException();
        }

        //Task<Models.Response> IActions.GetShortLink(Models.Request request)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
