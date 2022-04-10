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
{     
    public class PostService : IPostService
    {
         
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;
     
        public PostService(IPostRepository postRepository, IMapper mapper)  
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        public IQueryable<PostDto> GetAllPosts()
        {
            var posts = _postRepository.GetAll(); 
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
        public async Task<PostDto> AddNewPostAsync(CreatePostDto newPost, string userId)  
        {
             
            var post = _mapper.Map<Post>(newPost);
            post.UserId = userId;  
           var result =  await _postRepository.AddAsync(post);
            return _mapper.Map<PostDto>(result);
        }

     
        public async Task UpdatePostAsync(UpdatePostDto updatePost)
        {
            var existingPost = await _postRepository.GetByIdAsync(updatePost.Id);  
           var post = _mapper.Map(updatePost, existingPost);  
             
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

        public async Task<bool> UserOwnsPostAsync(int postId, string userId)
        {
              var post = await _postRepository.GetByIdAsync (postId);
            if (post == null) 
            { 
            return false;
            }
            if (post.UserId != userId)
            {
                return false;
            }

            return true;
        }
    }
}
