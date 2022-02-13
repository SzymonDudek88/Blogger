 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dto;
using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
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
        private readonly IMapper _mapper;
        //dodano nowy konstruktor
        // wstrzyknięto zaleznosc
        public PostService(IPostRepository postRepository, IMapper mapper) //ctor Imaper potrzebny do mapowania
            //wiec zeby wywolac klase PostService trzeba do niej uzyc jakiegos repozytorium typu Ipostrepository
            // i wtedy te repozytorium wstrzyykujemy w te klase i tutaj dzieja sie rzeczy ...
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        public IQueryable<PostDto> GetAllPosts()
        {
            var posts = _postRepository.GetAll();
            // gdy iqueryable do mapowania nalezy uzyc metody projectto, ona generuje tylko kod sql potrzebny do
            //zwracania odpowiendich pol , one sa badane i zwracane 

            return _mapper.ProjectTo<PostDto>(posts);
        }

        public async Task < IEnumerable<PostDto>> GetAllPostsAsync(int pageNumber, int pageSize, string sortField, bool ascending, string filterBy)
        {
            var posts = await _postRepository.GetAllAsync(  pageNumber,   pageSize, sortField, ascending, filterBy);

            return _mapper.Map<IEnumerable<PostDto>>(posts);
             #region
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
            #endregion  
        }




        public async Task<IEnumerable<PostDto>> GetPostByTitleContentAsync(string content)
        {
            var posts = await _postRepository.GetPostByTitleContentAsync(content);

            return  _mapper.Map<IEnumerable<PostDto>>(posts);
        }

        public async Task<PostDto> GetPostIdAsync(int id)
        {
            // jak moze wygladac implementacja hmmm

            var post = await _postRepository.GetByIdAsync(id);


            return _mapper.Map<PostDto>(post); 
            

        }
        public async Task<PostDto> AddNewPostAsync(CreatePostDto newPost)
        {
            if (newPost.Title == null)
            {
                throw new Exception("Post cant be empty");
            }
            var post = _mapper.Map<Post>(newPost);
           var result =  await _postRepository.AddAsync(post);
            return _mapper.Map<PostDto>(result);
        }

     
        public async Task UpdatePostAsync(UpdatePostDto updatePost)
        {
            var existingPost = await _postRepository.GetByIdAsync(updatePost.Id); // pobierasz post, ponizej go nadpisujesz
            //czyli on jest jakby po prostu wyciagniety z kolekcji i zmapowany
            //existing post jest typu Post
           var post = _mapper.Map(updatePost, existingPost); // wlasnie nie update post zrzucany jest na 
            //-----------------------------------------
            //mapowany post o tresci Updatepost i typie update post jest zmapowany typem na existin post a tresc ta sama
           // -----------------------------
            //wiec post powien byc typu   post i zawiera tresc z update post, wiec teraz tylko go przypisac:
             
           await _postRepository.UpdateAsync(post);
        }

        public async Task DeletePostAsync(int id)
        {
            var post = await  _postRepository.GetByIdAsync(id);
            await _postRepository.DeleteAsync(post);
        }

        public async Task<int> GetAllPostsCountAsync( string filterBy)
        {
            return await _postRepository.GetAllCountAsync(filterBy);
        }

     
    }
}
