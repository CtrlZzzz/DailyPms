using System;
using ClientServices.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ClientServices
{
    public static class DepndencyInjectionExtensions
    {
        public static IServiceCollection AddHttpClientServices(this IServiceCollection services)
        {
            return services.AddScoped<ISchoolService, HttpSchoolService>()
                           .AddScoped<IStudentService, HttpStudentService>();
        }
    }
}

