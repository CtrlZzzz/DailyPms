using DailyPmsAPI.Data;
using DailyPmsAPI.Repositories;
using DailyPmsShared;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace DailyPmsAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            //RegisterMongoDbClassMaps();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //  MongoDB
            services.AddSingleton<IMongoClient, MongoClient>(s =>
            {
                var connectionString = s.GetRequiredService<IConfiguration>()["ConnectionString"];
                return new MongoClient(connectionString);
            });

            //*TEMP - TO REMOVE :
            services.AddTransient<IDbContext, MongoDbContext>();
            services.AddTransient<ISchoolRepository, MongoSchoolRepository>();
            services.AddTransient<IClasseRepository, MongoClasseRepository>();
            services.AddTransient<IStudentRepository, MongoStudentRepository>();
            services.AddTransient<IPmsCenterRepository, MongoPmsCenterRepository>();
            services.AddTransient<IAgentRepository, MongoAgentRepository>();
            //*/

            //Db + Repositories
            services.AddTransient<IDatabase, MongoDatabase>();
            services.AddTransient<IRepository<School>, SchoolRepository>();
            //services.AddTransient<IRepository<Student>, StudentRepository>();
            //services.AddTransient<IRepository<Agent>, AgentRepository>();
            //services.AddTransient<IRepository<Class>, ClassRepository>();
            //services.AddTransient<IRepository<PmsCenter>, PmsCenterRepository>();
            //services.AddTransient<IRepository<PmsFile>, PmsFileRepository>();

            //services.AddTransient<SchoolRepository>(); // serviceCollection.AddTransient(typeof(IThing<>), typeof(GenericThing<>));?

            //services.AddTransient<StudentRepository>();
            //services.AddTransient<AgentRepository>();
            //services.AddTransient<ClassRepository>();
            //services.AddTransient<PmsCenterRepository>();
            //services.AddTransient<PmsFileRepository>();

            services.AddControllers();

            //  DEBUG api + client both on localhost (to avoid "Cross-origin" browser error)
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

            //   Swagger documentation
            services.AddSwaggerGen(c =>    // Register the swagger generator, defining one or more swagger documents
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

                //Use XML document to add infos in swagger doc
                var xmlFilePath = Path.Combine(AppContext.BaseDirectory, "DailyPmsAPI.xml");
                c.IncludeXmlComments(xmlFilePath);
            });

            //  Authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(Configuration.GetSection("AzureAdB2C"));

            //  Authorization
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireClaim("extension_Roles", "Admin"));
            });

            services.AddRazorPages();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();   // Enable middleware to serve generated swagger as JSON endpoint

            app.UseSwaggerUI(c =>   // Enable middleware to serve swagger-ui (HTML, JS, CSS, ...). Specifying the swagger JSON endpoint
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

        //void RegisterMongoDbClassMaps()
        //{
        //    BsonClassMap.RegisterClassMap<School>(cm =>
        //    {
        //        cm.AutoMap();
        //        cm.MapIdMember(s => s._id).SetIdGenerator(StringObjectIdGenerator.Instance)
        //                                  .SetSerializer(new StringSerializer(BsonType.ObjectId));
        //        cm.MapMember(s => s.PmsCenterID).SetSerializer(new StringSerializer(BsonType.ObjectId));
        //        cm.MapMember(s => s.ClasseIDs).SetSerializer(new EnumerableInterfaceImplementerSerializer<List<string>, string>(new StringSerializer(BsonType.ObjectId)));
        //        cm.MapMember(s => s.StudentIDs).SetSerializer(new EnumerableInterfaceImplementerSerializer<List<string>, string>(new StringSerializer(BsonType.ObjectId)));
        //    });
        //    BsonClassMap.RegisterClassMap<Classe>(cm =>
        //    {
        //        cm.AutoMap();
        //        cm.MapIdMember(c => c._id).SetIdGenerator(StringObjectIdGenerator.Instance)
        //                                       .SetSerializer(new StringSerializer(BsonType.ObjectId));
        //        cm.MapMember(c => c.SchoolID).SetSerializer(new StringSerializer(BsonType.ObjectId));
        //        cm.MapMember(c => c.PmsIDs).SetSerializer(new EnumerableInterfaceImplementerSerializer<List<string>, string>(new StringSerializer(BsonType.ObjectId)));
        //        cm.MapMember(c => c.StudentIDs).SetSerializer(new EnumerableInterfaceImplementerSerializer<List<string>, string>(new StringSerializer(BsonType.ObjectId)));
        //    });
        //    BsonClassMap.RegisterClassMap<Student>(cm =>
        //    {
        //        cm.AutoMap();
        //        cm.MapIdMember(s => s._id).SetIdGenerator(StringObjectIdGenerator.Instance)
        //                                  .SetSerializer(new StringSerializer(BsonType.ObjectId));
        //        //cm.MapMember(s => s.BirthDate).SetSerializer(new DateTimeSerializer(dateOnly: true));
        //        //cm.MapMember(s => s.RegistrationDate).SetSerializer(new DateTimeSerializer(dateOnly: true));
        //        cm.MapMember(s => s.SchoolID).SetSerializer(new StringSerializer(BsonType.ObjectId));
        //        cm.MapMember(s => s.ClasseID).SetSerializer(new StringSerializer(BsonType.ObjectId));
        //        cm.MapMember(s => s.PmsFileID).SetSerializer(new StringSerializer(BsonType.ObjectId));
        //    });
        //    BsonClassMap.RegisterClassMap<PmsFile>(cm =>
        //    {
        //        cm.AutoMap();
        //        cm.MapIdMember(p => p._id).SetIdGenerator(StringObjectIdGenerator.Instance)
        //                                        .SetSerializer(new StringSerializer(BsonType.ObjectId));
        //        cm.MapMember(p => p.StudentID).SetSerializer(new StringSerializer(BsonType.ObjectId));
        //        cm.MapMember(p => p.ActivityIDs).SetSerializer(new EnumerableInterfaceImplementerSerializer<List<string>, string>(new StringSerializer(BsonType.ObjectId)));
        //    });
        //    BsonClassMap.RegisterClassMap<PmsCenter>(cm =>
        //    {
        //        cm.AutoMap();
        //        cm.MapIdMember(c => c._id).SetIdGenerator(StringObjectIdGenerator.Instance)
        //                                       .SetSerializer(new StringSerializer(BsonType.ObjectId));
        //        cm.MapMember(c => c.DirectorID).SetSerializer(new StringSerializer(BsonType.ObjectId));
        //        cm.MapMember(c => c.PmsIDs).SetSerializer(new EnumerableInterfaceImplementerSerializer<List<string>, string>(new StringSerializer(BsonType.ObjectId)));
        //        cm.MapMember(c => c.SchoolIDs).SetSerializer(new EnumerableInterfaceImplementerSerializer<List<string>, string>(new StringSerializer(BsonType.ObjectId)));
        //    });
        //    BsonClassMap.RegisterClassMap<Agent>(cm =>
        //    {
        //        cm.AutoMap();
        //        cm.MapIdMember(a => a._id).SetIdGenerator(StringObjectIdGenerator.Instance)
        //                                      .SetSerializer(new StringSerializer(BsonType.ObjectId));
        //        cm.MapMember(a => a.CenterID).SetSerializer(new StringSerializer(BsonType.ObjectId));
        //        cm.MapMember(a => a.SchoolIDs).SetSerializer(new EnumerableInterfaceImplementerSerializer<List<string>, string>(new StringSerializer(BsonType.ObjectId)));
        //        cm.MapMember(a => a.PlanningIDs).SetSerializer(new EnumerableInterfaceImplementerSerializer<List<string>, string>(new StringSerializer(BsonType.ObjectId)));
        //        cm.MapMember(a => a.DailyIDs).SetSerializer(new EnumerableInterfaceImplementerSerializer<List<string>, string>(new StringSerializer(BsonType.ObjectId)));
        //    });
        //}
    }
}
