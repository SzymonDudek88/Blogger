using Infrastructute.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Wrappers;

namespace WebAPI.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public IdentityController(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            // usermanager od appuser - pozwala zarzadzac - wyszukiwac uzytkownika i dodawac nowego 
            _userManager = userManager;
            _configuration = configuration;
        }
        //sygnatura akcji register - sluzaca do rejestracji 
        [HttpPost]
        [Route("Register")]  // uzytkownik musi podac nazwe email i haslo
        // dlatego nalezy utworzyc klase modelu ktora bedzie wykorzystywnana do przesylania tych informacji
        // ona bedzie tylko do rejestracji , nie bedzie uzywana w systemie bo uzytkownika reprezentuje apliakcja application user
        // dlatego ta klasa bedzie w web api

        public async Task<IActionResult> Register(RegisterModel register)
        {
            // na poczatku sprawdzamy za pomoca findbynameasycn z usermanager czy uzytkownik nie jest juz zarejestrowany
            var userExist = await _userManager.FindByNameAsync(register.UserName);
            if (userExist != null)
            {
                //  w odpowiedzi zwracamy obiekt response rozszerzony o wiadomosc string = komunikat 
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<bool>
                {
                    Succeeded = false,
                    Message = "User alredy exists"

                });
            }

            ApplicationUser user = new ApplicationUser()
            {
                Email = register.Email,
                SecurityStamp = Guid.NewGuid().ToString(), // unikalny identyfikator, gdy zmieni sie cos zwiazanego z bezpieczenstwem
                //np haslo wtedy pliki cookie z logowania zostana uniewaznione
                UserName = register.UserName

            };

            var result = await _userManager.CreateAsync(user, register.Password); // zwraca IdentityResult ( to meta klasa)
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<bool>
                {
                    Succeeded = false,
                    Message = "Creation failed , check details and try again",
                    Errors = result.Errors.Select (x => x.Description )

                });
            }

            // zakonczono powodzeniem po przejsciu ifow 

            return Ok(new Response<bool> { Succeeded = true, Message = "User creation successfully " });

        }

    }
}
