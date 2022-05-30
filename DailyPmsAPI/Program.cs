using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;

namespace DailyPmsAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //        .ConfigureWebHostDefaults(webBuilder =>
        //        {
        //            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        //            if (environmentName == Environments.Development)
        //            {
        //                webBuilder.UseStartup<StartupTest>();
        //            }
        //            else
        //            {
        //                webBuilder.UseStartup<Startup>();
        //            }
        //        });

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
#if(DEBUG)
                    webBuilder.UseStartup<StartupTest>();
#else
                    webBuilder.UseStartup<Startup>();
#endif
                });
    }
}
