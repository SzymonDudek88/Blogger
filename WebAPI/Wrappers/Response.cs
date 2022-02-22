using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Wrappers
{
    public class Response <T> // generyczna klasa // i teraz nie wiem co ta klasa robi?
    {

        // w sumie to dosc proste, nowa klasa ktora zwraca w PostControllerze to samo tylko jako nowa instancje
        // z dodatkowym dopiskiem
        public T Data { get; set; }
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public IEnumerable<string> Errors { get; set;  } // pozwala na zwracanie komunikatow uzytkownikow a otrzymanych przez nas  

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
