using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Services;
using Domain.Interfaces;
using Infrastructute.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructute
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            //wstrzykujemy implementacje: jaka konkretna implementacja ma byc w wykorzystana w przypadku
            //danego interfejsu
            //addscoped - implementacja bedzie pojedyncza per cale żądanie HTTP
            services.AddScoped<IPostRepository, PostRepository>();
            //po powyzszym framework asp . net core bedzie wiedzial ze gdzie kolwiek na wejsciu dostanei 
            //interface ipostrepository to przypisze do niego implementacje klasy PostRepository
            return services;

        }
    }
}
 
