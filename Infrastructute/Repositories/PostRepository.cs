using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructute.Data;
using Infrastructute.ExtensionMethods;
using Microsoft.EntityFrameworkCore;

namespace Infrastructute.Repositories
{
    public class PostRepository : IPostRepository

        
    {
        private readonly BloggerContext _context;
        public PostRepository(BloggerContext context)
        {
            _context = context;
        }

        public   IQueryable<Post> GetAll()
        {
            return _context.Posts.AsQueryable();  
        }

        public async Task< IEnumerable<Post>> GetAllAsync(int pageNumber, int pageSize, string sortField, bool ascending, string filterBy)
        {
            // tutaj takze stosujemy metode ktora na gotowo ustawialismy do sortowania
            //dzieki skip i take pobieramy odpowiednie posty
            return await _context.Posts
                .Where (m => m.Title.ToLower().Contains(filterBy.ToLower()) || m.Content.ToLower().Contains(filterBy.ToLower()))
                .OrderByPropertyName(sortField,ascending)
                .Skip((pageNumber - 1 ) * pageSize)
                .Take(pageSize).ToListAsync()    ;
        }
        public async Task<int> GetAllCountAsync(string filterBy)
        {
            return await _context.Posts.Where(m => m.Title.ToLower().Contains(filterBy.ToLower()) || m.Content.ToLower().Contains(filterBy.ToLower())).CountAsync();
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
        public async Task UpdateAsync(Post post)  
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

        public async Task<IEnumerable<Post>> GetPostByTitleContentAsync(string content)  
        {
            //  bool stringIsOnlyWhiteSpaces = String.IsNullOrWhiteSpace(content);
             
            var temp =  from a in _context.Posts  
                   where a.Title.ToLower().Contains(content.ToLower())
                   select a;
            return await temp.ToListAsync();  
 
        }

      
    }
}
