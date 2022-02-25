using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Application.Services
{
    public  class UserResolverService
    {
        // ta klasa odpowiada za sprawdzenie nazwy uzytkownika aby mozna ja bylo wykorzystac np do przypisania posta uzytkownikowi 
        // ponizszy typ to element dodatkowej biblioteki zainstalowanej nuget
        // wstrzykuje w _context to co dzieje sie chyba w post service bo tam uzywasz http 
        private readonly IHttpContextAccessor _context;
        // ta klasa odpowiada za przekazanie - wstrzykniecie informacji do klasy odpowiedzialnej za informacje o czasie do 
        public UserResolverService(IHttpContextAccessor context)  // paczka
        {
            _context = context;
        }

        public string GetUser()
        { 
        return _context.HttpContext.User?.Identity?.Name;

            // teraz trzeba to dopisac w MVC Installer 
        
        }

    }
}
