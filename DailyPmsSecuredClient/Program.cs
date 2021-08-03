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
using MudBlazor.Services;

namespace DailyPmsSecuredClient
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            var apiUrl = builder.Configuration.GetValue<string>("ApiUrl");
            //  HttpClient that automatically includes access token when making requests to the server
            builder.Services.AddHttpClient("DailyPmsAPI", client => client.BaseAddress = new Uri(apiUrl))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("DailyPmsAPI"));

            // MudBlazor
            builder.Services.AddMudServices();

            //  Msal
            var defaultScopeID = builder.Configuration.GetValue<string>("ApiDefaultScope");
            builder.Services.AddMsalAuthentication(options =>
            {
                builder.Configuration.Bind("AzureAdB2C", options.ProviderOptions.Authentication);
                options.ProviderOptions.DefaultAccessTokenScopes.Add(defaultScopeID);

                //  AdB2C Login in the same page instead of a pop up window
                options.ProviderOptions.LoginMode = "redirect";
                //  Roles
                options.UserOptions.RoleClaim = "appRole";
            });

            //builder.Services.AddAuthorizationCore(options =>
            //{
            //    options.AddPolicy("Admin", policy => policy.RequireClaim("extension_Roles", "Admin"));
            //});
            builder.Services.AddAuthorizationCore(options =>
            {
                options.AddPolicy("Directeur", policy => policy.RequireClaim("jobTitle", "directeur"));
            });

            await builder.Build().RunAsync();
        }
    }
}
