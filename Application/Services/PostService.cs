using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using Domain.Interfaces;

namespace Application.Services
{       // zwraca dane w postaci PostDTO
    // pozostalo wstrzykiwanie zaleznosci 
     // W TEJ KLASIE RZUTUJESZ NA PostDTO pobierajac z postRepository typu IPostRepository ktore pracuje na POST


    //my do tej pory nigdzie tej klasy nie uzywamy, tworzymy ja  i ona przechowuje cos podobnie jak klasa
    //Postrepository - z infrastructure 
    public class PostService : IPostService
    {
        //czyli rzutujemy lub wstrzykujemy ipostrepository w ktorym przechowujemy wszystkie posty na ten tutaj
        //zbior postow

        //przypisujemy do zmiennej aby nie mozna bylo nic z nia zrobic:
        private readonly IPostRepository _postRepository;
        private readonly IMapper _imapper;
        //dodano nowy konstruktor
        // wstrzyknięto zaleznosc
        public PostService(IPostRepository postRepository, IMapper mapper) //ctor Imaper potrzebny do mapowania
            //wiec zeby wywolac klase PostService trzeba do niej uzyc jakiegos repozytorium typu Ipostrepository
            // i wtedy te repozytorium wstrzyykujemy w te klase i tutaj dzieja sie rzeczy ...
        {
            _postRepository = postRepository;
            _imapper = mapper;
        }
        public IEnumerable<PostDto> GetAllPosts()
        {
            var posts = _postRepository.GetAll();

            return _imapper.Map<IEnumerable<PostDto>>(posts);
            
            //reczne mapowanie:
            //return posts.Select(x => new PostDto //dla kazdesgo reprezentanta posts tworzysz
            //// nowa instancje PostDto o ponizszych parametrach - kazdy jeden parametr jest przepisany z 
            ////x - reprezentanta branego po kolei i przypisywana do kazdego kolejnego Postdo 
            //// i zwraca to typ Ienumerable jak w deklaracji, czyli cos prostszego niz lista - zbiór
            //{ 
            //Id= x.Id,
            //Title = x.Title,
            //Content = x.Content
            //});

            //return from a in posts
            //       select new PostDto
            //       {
            //           Id = a.Id,
            //           Title = a.Title,
            //           Content = a.Content
            //       };

        }

        public PostDto GetPostId(int id)
        {
            // jak moze wygladac implementacja hmmm

            var post = _postRepository.GetById(id);


            return _imapper.Map<PostDto>(post); 
            //reczne mapowanie
            //return new PostDto()
            //{
            //    Id = post.Id,
            //    Title = post.Title,
            //    Content = post.Content

            //};

        } 
               
    }
}
