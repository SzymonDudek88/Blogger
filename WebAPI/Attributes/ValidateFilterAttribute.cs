using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using WebAPI.Wrappers;

namespace WebAPI.Attributes
{ // l5 fluetn validation - ta klasa ma cos wspolnego z poprawnoscia wyswietlania komunikatow dla niepoprawnych
    // tresci bledów
    public class ValidateFilterAttribute : ResultFilterAttribute
    {

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            base.OnResultExecuting(context);
            // sprawdzamy czy validacja zakończyła się nie powidzeniem 
            if (!context.ModelState.IsValid)
            {
                // jezeli tak jest tzn zsakonczyla sie bledem to pobieramy info o bledach i odpowiednio formatuje
                //my zwracany rezultat 
                var entry = context.ModelState.Values.FirstOrDefault();

                context.Result = new BadRequestObjectResult(new Response<bool>
                {
                    Succeeded = false,

                    Message = "Someting went wrong from validate filter atrubie cs.",
                    Errors = entry.Errors.Select(x => x.ErrorMessage)

                });
            
            }
        }
    }
}
