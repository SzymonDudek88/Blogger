using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Wrappers
{
    public class Response <T> // generyczna klasa 
    {

        // w sumie to dosc proste, nowa klasa ktora zwraca w PostControllerze to samo tylko jako nowa instancje
        // z dodatkowym dopiskiem
        public T Data { get; set; }
        public bool Succeeded { get; set; }

        public Response()
        {

        }

        public Response(T data)
        {
            Data = data;
            Succeeded = true; // poprawne wykonanie zadania - wyswietla przy operacjach 
        }
    }
}
