using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using DailyPmsClient.Services;
using MudBlazor.Services;

namespace DailyPmsClient
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            ////Standard Httpclient 
            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            ////HttpclientFactory
            builder.Services.AddHttpClient<IStudentDataService, StudentDataService>(client =>
            {
                var apiUrl = builder.Configuration.GetValue<string>("ApiUrl");
                //var apiUrl = "https://localhost:5001/";
                client.BaseAddress = new Uri(apiUrl);
            });

            builder.Services.AddMudServices();

            await builder.Build().RunAsync();
        }
    }
}
