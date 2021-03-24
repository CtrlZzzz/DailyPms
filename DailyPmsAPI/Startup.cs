using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using DailyPmsAPI.Data;
using MongoDB.Bson.Serialization;
using DailyPmsAPI.Models;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson;

namespace DailyPmsAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            BsonClassMap.RegisterClassMap<School>(cm =>
            {
                cm.AutoMap();
                cm.MapIdMember(s => s.SchoolId).SetIdGenerator(StringObjectIdGenerator.Instance);
                cm.IdMemberMap.SetSerializer(new StringSerializer(BsonType.ObjectId));
                cm.MapMember(s => s.ClasseIDs).SetSerializer(new StringSerializer(BsonType.ObjectId));
                cm.MapMember(s => s.StudentIDs).SetSerializer(new StringSerializer(BsonType.ObjectId));
            });
            BsonClassMap.RegisterClassMap<Classe>(cm =>
            {
                cm.AutoMap();
                cm.MapIdMember(s => s.ClasseId).SetIdGenerator(StringObjectIdGenerator.Instance);
                cm.IdMemberMap.SetSerializer(new StringSerializer(BsonType.ObjectId));
                cm.MapMember(s => s.SchoolId).SetSerializer(new StringSerializer(BsonType.ObjectId));
                cm.MapMember(s => s.PmsIDs).SetSerializer(new StringSerializer(BsonType.ObjectId));
                cm.MapMember(s => s.StudentIDs).SetSerializer(new StringSerializer(BsonType.ObjectId));
            });
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IMongoClient, MongoClient>(s =>
            {
                var connectionString = s.GetRequiredService<IConfiguration>()["ConnectionString"];
                return new MongoClient(connectionString);
            });
            services.AddSingleton<IDbContext, MongoDbContext>();
            services.AddSingleton<ISchoolRepository, MongoSchoolRepository>();

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
