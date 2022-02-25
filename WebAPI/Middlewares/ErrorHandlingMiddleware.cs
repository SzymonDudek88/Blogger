using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using WebAPI.Wrappers;

namespace WebAPI.Middlewares
{
    public class ErrorHandlingMiddleware : IMiddleware // interface pozwalajacy programowi rozpoznac ze to middleware 
    {
        // kazde zapytanie ktore idzie do api bedzie procesowane przez middleware 
        public async Task InvokeAsync(HttpContext context, RequestDelegate next) // dostep do http kontekst, i innego midleware 
        {
            try
            {
                await next.Invoke(context); // w tym momencie on wywoluje po prostu innego middleware 

            }
            catch ( Exception ex )
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync(new Response(false, ex.Message)); // jeszcze zainstalowac w mvc installer 
            }

        }
    }
}
