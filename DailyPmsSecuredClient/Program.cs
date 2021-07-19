using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DailyPmsSecuredClient
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            var apiUrl = builder.Configuration.GetValue<string>("ApiUrl");
            builder.Services.AddHttpClient("DailyPmsAPI", client => client.BaseAddress = new Uri(apiUrl))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            //  HttpClient that automatically includes access token when making requests to the server
            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("DailyPmsAPI"));

            //  Msal
            var defaultScopeID = builder.Configuration.GetValue<string>("ApiDefaultScope");
            builder.Services.AddMsalAuthentication(options =>
            {
                builder.Configuration.Bind("AzureAdB2C", options.ProviderOptions.Authentication);
                //options.ProviderOptions.DefaultAccessTokenScopes.Add(defaultScopeID);
                options.ProviderOptions.DefaultAccessTokenScopes.Add("https://DailyPms.onmicrosoft.com/a0ea78d4-e079-496f-a76c-5cf6e10a5054/DailyPms.ReadWrite");
            });

            await builder.Build().RunAsync();
        }
    }
}
