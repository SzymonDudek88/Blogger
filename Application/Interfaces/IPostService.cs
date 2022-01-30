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
        // nvm dlaczego dotyczy tylko 2 metod ...
       Task < IEnumerable<PostDto>> GetAllPostsAsync();

       Task < IEnumerable<PostDto>> GetPostByTitleContentAsync(string content);
        Task<PostDto> GetPostIdAsync(int id);
        //Metody serwisu nie moga zwracac modeli domenowych
        //metody tutaj beda wywolywane w webAPI - a te nie powinno wiedziec nic co dzieje sie w warstwie domenowej
        // modele domenowe powinny byc enkapsulowane w samej domenie
        // wszystko co trzeba bedzie zmapowane zeby byc zwracane do warstwy prezentacji
        //dlatego tworzymy osobne klasy ktore beda przesylac do webAPI
        //czyli DTO

        //i tutaj podmieniono Post na PostDto
        Task<PostDto> AddNewPostAsync(CreatePostDto newPost);
        Task UpdatePostAsync( UpdatePostDto updatePost);
        Task DeletePostAsync ( int id );

    }
}
