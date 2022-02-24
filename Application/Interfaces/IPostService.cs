using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dto;
using Application.DTO;
using Domain.Entities;

namespace Application.Interfaces
{
   public interface IPostService 
    {
        IQueryable<PostDto> GetAllPosts();
        // nvm dlaczego dotyczy tylko 2 metod ...
        Task<IEnumerable<PostDto>> GetAllPostsAsync(int pageNumber, int pageSize, string sortField, bool ascending, string filterBy);
       Task <  int> GetAllPostsCountAsync( string filterBy);

       Task < IEnumerable<PostDto>> GetPostByTitleContentAsync(string content);
        Task<PostDto> GetPostIdAsync(int id);
        //Metody serwisu nie moga zwracac modeli domenowych
        //metody tutaj beda wywolywane w webAPI - a te nie powinno wiedziec nic co dzieje sie w warstwie domenowej
        // modele domenowe powinny byc enkapsulowane w samej domenie
        // wszystko co trzeba bedzie zmapowane zeby byc zwracane do warstwy prezentacji
        //dlatego tworzymy osobne klasy ktore beda przesylac do webAPI
        //czyli DTO

        //i tutaj podmieniono Post na PostDto
        Task<PostDto> AddNewPostAsync(CreatePostDto newPost, string userId); // userID z db chyba do logowania 
        Task UpdatePostAsync( UpdatePostDto updatePost);
        Task DeletePostAsync ( int id );

    }
}
