 
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
 
        }
        public async Task<Post> AddAsync(Post post)
        {
            var sql = "INSERT INTO Posts (Title, Content, Created, LastModified) VALUES (@Title, @Content, @Created, @LastModified);"
              + "SELECT CAST(SCOPE_IDENTITY() as int); ";
            var id = await db.QueryFirstOrDefaultAsync<int>(
                sql,
                new
                {
                    post.Title,
                    post.Content,
                    @Created = DateTime.UtcNow,
                    @LastModified = DateTime.UtcNow
                }).ConfigureAwait(false);

            post.Id = id;
             
            return post;
        }
        public async Task UpdateAsync(Post post)
        {
            var sql =  "UPDATE Posts SET Content = @Content WHERE Id = @Id";
            await db.ExecuteAsync(sql, new
            {
                post.Content,
                @Id = post.Id
            }).ConfigureAwait(false);
           
        }
        public async Task DeleteAsync(Post post)
        {
            var sql = "DELETE FROM Posts WHERE Id = @id";
          await  db.ExecuteAsync(sql, new { @id =  post.Id }).ConfigureAwait(false);
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
