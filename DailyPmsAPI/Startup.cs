using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Azure.Storage.Blobs;
using DailyPmsAPI.Data;
using DailyPmsAPI.Repositories;
using DailyPmsAPI.Sql;
using DailyPmsShared;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using System;
using System.IO;
using IGeekFan.AspNetCore.RapiDoc;

namespace DailyPmsAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IMongoClient, MongoClient>(s =>
            {
                var connectionString = GetVaultSecret("MongoDbConnectionString");
                return new MongoClient(connectionString);
            });

            services.AddDbContext<SqlDbContext>(options =>
            {
                var connectionStringSql = GetVaultSecret("AzureSqlDbConnectionString");
                options.UseSqlServer(connectionStringSql);
            });

            services.AddSingleton(x =>
            {
                var blobStorageConnectionString = GetVaultSecret("BlobStorageConnectionString");
                return new BlobServiceClient(blobStorageConnectionString);
            });

            services.AddTransient<IDatabase, MongoDatabase>();
            services.AddTransient<IRepository<School>, SchoolRepository>();
            services.AddTransient<IRepository<Student>, StudentRepository>();
            services.AddTransient<IRepository<PmsFile>, PmsFileRepository>();
            services.AddTransient<IRepository<PmsCenter>, PmsCenterRepository>();
            services.AddTransient<IRepository<Class>, ClassRepository>();
            services.AddTransient<IRepository<Agent>, AgentRepository>();


            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins("https://localhost:44367")
                                .AllowAnyHeader()
                                .AllowAnyMethod()
                                .AllowCredentials();
                    });
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Daily PMS API Documentation",
                    Version = "v1",
                    Description = "Daily PMS allows your app to use the dailyPMS Database (mongoDb). " +
                                  "Note : All students, classes, schools,..., and all other informations from the database are fictitious " +
                                  "and generated with 'Online Custom Mock Data Generator' online toolkit " +
                                  "(https://www.onlinewebtoolkit.com/generatedata/).",
                    Contact = new OpenApiContact
                    {
                        Name = "Mobile Inception (Mi8)",
                        Email = "info@mi8.be",
                        Url = new Uri("https://www.mi8.be")
                    }
                });

                var xmlFilePath = Path.Combine(AppContext.BaseDirectory, "DailyPmsAPI.xml");
                c.IncludeXmlComments(xmlFilePath, true);
            });

            services.AddRazorPages();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseRapiDocUI(c =>
            {
                c.RoutePrefix = String.Empty;
                c.SwaggerEndpoint("/v1/api-docs", "Daily PMS API Documentation v1");
                c.GenericRapiConfig = new GenericRapiConfig()
                {
                    FillRequestFieldsWithExample = true,
                    Theme = "light",
                    RenderStyle = "read",
                    UsePathInNavBar = false,
                    ShowComponents = true,
                    SchemaStyle = "table",
                    AllowServerSelection = false,
                    AllowAuthentication = false
                };
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
                endpoints.MapFallbackToFile("index.html");
                endpoints.MapSwagger("{documentName}/api-docs");
            });
        }

        string GetVaultSecret(string secretName)
        {
            var client = new SecretClient(new Uri("https://dailypmsvault.vault.azure.net/"), new ManagedIdentityCredential());
            var secret = client.GetSecret(secretName).Value;

            return secret.Value;
        }
    }
}
