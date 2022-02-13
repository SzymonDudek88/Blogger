﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application;
using Application.Interfaces;
using Application.Mappings;
using Application.Services;
using Domain.Interfaces;
using Infrastructute;
using Infrastructute.Repositories;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebAPI.Installers
{
    public class MvcInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {


            services.AddApplication();
            services.AddInfrastructure();
            services.AddControllers()
                .AddJsonOptions( options =>
                {
                    options.JsonSerializerOptions.WriteIndented = true;
                }
                );

            services.AddApiVersioning(x => {
                x.DefaultApiVersion = new ApiVersion(1,0); // odpowiada za wersje
                x.AssumeDefaultVersionWhenUnspecified = true;
                x.ReportApiVersions = true;
             // x.ApiVersionReader = new HeaderApiVersionReader("x-api-version");
                // to z lekcji o postmanie chyba jest i musi to byc bo inaczej sie nie odpala, natomiast po 
                //cosmo db nie chce za cholere dzialac wiec nie ma co pomijac... za drugim razem dziala xD
            });

            services.AddOData();
        }
    }
}
