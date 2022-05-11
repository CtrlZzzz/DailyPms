using Azure.Storage.Blobs;
using DailyPmsAPI.Data;
using DailyPmsAPI.Repositories;
using DailyPmsAPI.Sql;
using DailyPmsShared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using System;
using System.IO;


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
                var connectionString = Configuration.GetConnectionString("MongoConnection");
                return new MongoClient(connectionString);
            });

            services.AddDbContext<SqlDbContext>(options =>
            {
                var connectionStringSql = Configuration.GetConnectionString("SqlConnection");
                options.UseSqlServer(connectionStringSql);
            });

            services.AddSingleton(x =>
            {
                var blobStorageConnectionString = Configuration.GetConnectionString("BlobStorageConnectionString");
                return new BlobServiceClient(blobStorageConnectionString);
            });

            //   TO DO => Old repo - non generic, to replace with generic one
            services.AddTransient<IDbContext, MongoDbContext>();
            services.AddTransient<IClasseRepository, MongoClasseRepository>();
            services.AddTransient<IPmsCenterRepository, MongoPmsCenterRepository>();
            services.AddTransient<IAgentRepository, MongoAgentRepository>();
            //

            services.AddTransient<IDatabase, MongoDatabase>();
            services.AddTransient<IRepository<School>, SchoolRepository>();
            services.AddTransient<IRepository<Student>, StudentRepository>();
            

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
                c.IncludeXmlComments(xmlFilePath);
            });

            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddMicrosoftIdentityWebApi(Configuration.GetSection("AzureAdB2C"));

            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("Admin", policy => policy.RequireClaim("extension_Roles", "Admin"));
            //});

            services.AddRazorPages();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Daily PMS API Documentation v1");
                c.RoutePrefix = string.Empty;
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
            });
        }
    }
}
