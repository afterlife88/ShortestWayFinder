using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShortestWayFinder.Domain;
using ShortestWayFinder.Domain.Infrastructure.Algorithms;
using ShortestWayFinder.Domain.Infrastructure.Configuration;
using ShortestWayFinder.Domain.Infrastructure.Contracts;
using ShortestWayFinder.Domain.Infrastructure.Repositories;
using ShortestWayFinder.Web.Configuration;
using ShortestWayFinder.Web.Contracts;
using ShortestWayFinder.Web.Services;

namespace ShortestWayFinder.Web
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataDbContext>(opt => opt.UseInMemoryDatabase());

            services.AddTransient<IDatabaseInitializer, DatabaseInitializer>();
            services.AddScoped<IPathRepository, PathRepository>();
            services.AddScoped<IPathService, PathService>();
            services.AddScoped<IShortestPath, ShortestPathAlgorithm>();
            services.AddMvc();
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

            // Recreate db's
            databaseInitializer.Seed().GetAwaiter().GetResult();
        }
    }
}
