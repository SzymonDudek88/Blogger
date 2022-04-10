using Infrastructute.Data;
using Infrastructute.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Installers
{
    public class IdentityInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {

            // rejestrujemy usługe identity:
            services.AddIdentity<ApplicationUser, IdentityRole>()

                //dzieki temu ze podajemy entity framework jako magazyn to dodajac migracje framework 
                // asp . net core identity utworzy przez nas tabele potrzebne do uwierzytelniania i weryfikacji
                // to bedzie widac w serverze
                .AddEntityFrameworkStores<BloggerContext>()
                .AddDefaultTokenProviders(); // Add-Migration AddIdentityTables   


            // okreslamy schemat uwierzytelniania przez zarejestrowanie uslugi uwierzytelniania 
            // wywolujac metode rozszerzajaca:

            // potem ustalamy uzycie standardu jwt

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            })

             //następnie poprzez wywołanie metody add jwt bearer wlaczamy uwierzytelnianie jwt 
             // odbywa sie ono poprzez wyodrebnienie i walidacje tokena jwt tokena z naglowka zadania autoryzacji
             // KROPKA!!!
             .AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new TokenValidationParameters() {
                     // japierw ustawiamy wartosci logiczne kontrolujace czy wystawca i odbiorca zostana zweryfikowani podczas
                     // walidacji tokena, my ustawiamy falsz - bo nie potrzeba okreslac na jakiej witrynie token jest generowany
                     // oraz z jakiej witrny pochodza uzytkownicy chcacy sie uwierzytelnic:
                    ValidateIssuer = false,
                    ValidateAudience = false,
                     // najwazniejsze 256 bitowy sekretny klucz uzywany do sprawdzania poprawnosci popisu
                     // ustawion w appsetting json i z tamtad pobrany zakodowany w utf8
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))


            };


             }
                
          );
        }
    }
}
