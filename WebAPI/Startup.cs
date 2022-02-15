using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTO;
using Application.Interfaces;
using Application.Mappings;
using Application.Services;
using Domain.Interfaces;
using Infrastructute.Repositories;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OData;
using Microsoft.OData.Edm;
using Microsoft.OpenApi.Models;
using WebAPI.Installers;

namespace WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.InstallServicesInAssembly(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
           

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.EnableDependencyInjection(); // wstrzykiwanie zaleznosci dla tras http
                endpoints.Filter().OrderBy().MaxTop(10); // definujemy mozliwe do wykonania operacje
                endpoints.MapODataRoute("odata","odata",GetEdmModel());    // definiujemy rooting, jaki model jest reprezentowany przez jaka encje


                endpoints.MapControllers();
            });
        }

        public static IEdmModel GetEdmModel( )
        {
            var builder = new ODataConventionModelBuilder();
            builder.EntitySet<PostDto>("Posts");               //wskazujemy ze encje post bedzie reprezentowal model postdto
            return builder.GetEdmModel();
        }
    }
}
