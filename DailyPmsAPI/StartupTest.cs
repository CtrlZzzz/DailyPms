using DailyPmsAPI.Data;
using DailyPmsAPI.Repositories;
using DailyPmsShared;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;

[assembly: Microsoft.Extensions.Configuration.UserSecrets.UserSecretsId("34705a46-8913-4be5-933f-09b446114319")]
namespace DailyPmsAPI
{
    public class StartupTest
    {
        public StartupTest()
        {
            //Configuration = configuration;

            Configuration = new ConfigurationBuilder()
                .AddUserSecrets<StartupTest>(false, true)
                .Build();
        }

        public IConfiguration Configuration { get; set; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IMongoClient, MongoClient>(s =>
            {
                var connectionString = GetUserSecret("ConnectionStrings:MongoConnection");
                return new MongoClient(connectionString);
            });

            //services.AddDbContext<SqlDbContext>(options =>
            //{
            //    var connectionStringSql = Configuration.GetConnectionString("SqlConnection");
            //    options.UseSqlServer(connectionStringSql);
            //});

            //services.AddSingleton(x =>
            //{
            //    var blobStorageConnectionString = Configuration.GetConnectionString("BlobStorageConnectionString");
            //    return new BlobServiceClient(blobStorageConnectionString);
            //});

            //TO DO => Old repo -non generic, to replace with generic one
            services.AddTransient<IDbContext, MongoDbContext>();
            services.AddTransient<IClasseRepository, MongoClasseRepository>();
            services.AddTransient<IPmsCenterRepository, MongoPmsCenterRepository>();
            services.AddTransient<IAgentRepository, MongoAgentRepository>();
            //

#if (DEBUG)
            services.AddTransient<IDatabase, MongoTestDatabase>();
#else
            services.AddTransient<IDatabase, MongoDatabase>();
#endif
            //services.AddTransient<IDatabase, MongoDatabase>();
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

            services.AddRazorPages();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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

        //string GetVaultSecret(string secretName)
        //{
        //    var client = new SecretClient(new Uri("https://dailypmsvault.vault.azure.net/"), new ManagedIdentityCredential());
        //    var secret = client.GetSecret(secretName).Value;

        //    return secret.Value;
        //}

        string GetUserSecret(string secretName)
        {
            var userSecret = Configuration[secretName];
            return userSecret;
        }
    }
}

