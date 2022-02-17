 
using Dapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
 

namespace Infrastructute.Repositories
{
    class DapperPostRepository : IPostRepository
    {

        private IDbConnection db;

        public DapperPostRepository(IConfiguration configuration)
        {
            this.db = new SqlConnection(configuration.GetConnectionString("BloggerCS"));
        }

       public IQueryable<Post> GetAll()
        {
            var sql = "SELECT * FROM Posts";
             return db.Query<Post>(sql).AsQueryable(); //xD
        }

        

        public  async  Task<Post> GetByIdAsync(int id)
        {
     
           var sql = "SELECT * FROM Posts WHERE Id = @id";
            return  await db.QuerySingleOrDefaultAsync<Post>(sql, new { id }).ConfigureAwait(false) ; // zwraca pojedynczy obiekt

            //   return await _context.Posts.SingleOrDefaultAsync(x => x.Id == id);
        }
        public Task<Post> AddAsync(Post post)
        {
            throw new NotImplementedException();
        }
        public Task UpdateAsync(Post post)
        {
            throw new NotImplementedException();
        }
        public Task DeleteAsync(Post post)
        {
            throw new NotImplementedException();
        }
      






       

        Task<IEnumerable<Post>> IPostRepository.GetAllAsync(int pageNumber, int pageSize, string sortField, bool ascending, string filterBy)
        {
            throw new NotImplementedException();
        }

        Task<int> IPostRepository.GetAllCountAsync(string filterBy)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Post>> IPostRepository.GetPostByTitleContentAsync(string content)
        {
            throw new NotImplementedException();
        }
    }
}
