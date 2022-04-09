using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructute.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
 
 

namespace WebAPI.Installers
{
    public class DbInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration Configuration)
        {
          //  services.AddDbContext<BloggerContext>(options =>
            // options.UseSqlServer(Configuration.GetConnectionString("BloggerCS")));

            services.AddDbContext<BloggerContext>(opt =>
                 opt.UseInMemoryDatabase("BloggerCS"));
        }
    }
    
}
