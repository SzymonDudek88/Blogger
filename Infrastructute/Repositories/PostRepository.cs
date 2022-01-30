using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructute.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructute.Repositories
{
    public class PostRepository : IPostRepository

        //to jest glowna klasa przechowywania postow i od niej masz inne ruchy
    {
        private readonly BloggerContext _context;
        public PostRepository(BloggerContext context)
        {
            _context = context;
        }


        public async Task< IEnumerable<Post>> GetAllAsync()
        {
            return await _context.Posts.ToListAsync()    ;
        }

        public async Task<Post> GetByIdAsync(int id)
        {
            return await _context.Posts.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Post> AddAsync(Post post)
        {
            
          post.Created = DateTime.UtcNow ;
            var createdPost = await _context.Posts.AddAsync(post); // po zmianach
           await _context.SaveChangesAsync(); // zapisuje zmiany w bazie danych - koneiczne
            return createdPost.Entity;
        }
        public async Task UpdateAsync(Post post) // i co zmienimy tu cos?
        {
            _context.Posts.Update(post);
           await _context.SaveChangesAsync();
            await Task.CompletedTask;
        }
        public async Task DeleteAsync(Post post)
        {
            _context.Posts.Remove(post);
          await  _context.SaveChangesAsync();
            await Task.CompletedTask;

        }

        public async Task<IEnumerable<Post>> GetPostByTitleContentAsync(string content) //////////// tu LOGIKA CALA NIGDZIE INDZIEJ
        {
            //  bool stringIsOnlyWhiteSpaces = String.IsNullOrWhiteSpace(content);
             
            var temp =  from a in _context.Posts  
                   where a.Title.ToLower().Contains(content.ToLower())
                   select a;
            return await temp.ToListAsync(); // xD



        }
    }
}
