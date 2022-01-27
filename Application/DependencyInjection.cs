using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Mappings;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
   public static  class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services) {

            services.AddScoped<IPostService, PostService>(); 
            //singleton zapewnia ze implementacja bedzie tworzona tylko 1 raz
            services.AddSingleton(AutoMapperConfig.Initialize());
            //teraz framework bedzie wiedzial ze jak dostanie interface i maper to wstrzyknie implementacje automapconfig  
            return services;
        }
    }
}
