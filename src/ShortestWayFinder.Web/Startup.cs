using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShortestWayFinder.Domain;
using ShortestWayFinder.Domain.Infrastructure.Contracts;

namespace ShortestWayFinder.Web
{
    public class Startup
    {
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataDbContext>(opt => opt.UseInMemoryDatabase());

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IDatabaseInitializer databaseInitializer)
        {
            app.UseStaticFiles();
            app.UseCors(builder =>
                // This will allow any request from any server. 
                builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin());

            // Add MVC to the request pipeline.
            app.UseDeveloperExceptionPage();
            app.UseMvc();
        }
    }
}
