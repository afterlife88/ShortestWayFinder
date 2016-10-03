using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json.Serialization;
using ShortestWayFinder.Domain;
using ShortestWayFinder.Domain.Infrastructure.Algorithms;
using ShortestWayFinder.Domain.Infrastructure.Configuration;
using ShortestWayFinder.Domain.Infrastructure.Contracts;
using ShortestWayFinder.Domain.Infrastructure.Repositories;
using ShortestWayFinder.Web.Configuration;
using ShortestWayFinder.Web.Contracts;
using ShortestWayFinder.Web.Services;
using Swashbuckle.Swagger.Model;

namespace ShortestWayFinder.Web
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var connectionStringConfig = builder.Build();

            services.AddDbContext<DataDbContext>(opt => opt.UseSqlServer(
                connectionStringConfig.GetConnectionString("DefaultConnection")));

            services.AddTransient<IDatabaseInitializer, DatabaseInitializer>();
            services.AddScoped<IPathRepository, PathRepository>();
            services.AddScoped<IPathService, PathService>();
            services.AddScoped<IShortestPath, ShortestPathAlgorithm>();
            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

            services.AddSwaggerGen(options =>
            {
                options.SingleApiVersion(new Info
                {
                    Version = "v1",
                    Title = "Shortest way finder",
                    Description = "API documentation",
                    TermsOfService = "None"
                });
                options.IncludeXmlComments(GetXmlCommentsPath(PlatformServices.Default.Application));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IDatabaseInitializer databaseInitializer)
        {
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseCors(builder =>
                // This will allow any request from any server. 
                builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin());

            AutomapperConfiguration.Load();

            // Add MVC to the request pipeline.
            app.UseDeveloperExceptionPage();
            app.UseMvc();

            app.UseSwagger((httpRequest, swaggerDoc) =>
            {
                swaggerDoc.Host = httpRequest.Host.Value;
            });

            app.UseSwaggerUi();
            app.UseMvcWithDefaultRoute();

            // Seed data to database (commented because uses not local sql database)
            // Change 29 line to use UseInMemoryDatabase and uncomment line bellow to reseed data.
            // databaseInitializer.Seed().GetAwaiter().GetResult();
        }

        private string GetXmlCommentsPath(ApplicationEnvironment appEnvironment)
        {
            return Path.Combine(appEnvironment.ApplicationBasePath, "ShortestWayFinder.Web.xml");
        }
    }
}
