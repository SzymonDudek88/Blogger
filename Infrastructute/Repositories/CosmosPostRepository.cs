using Cosmonaut;
using Cosmonaut.Extensions;
using Domain.Entities.Cosmos;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructute.Repositories
{
    public class CosmosPostRepository : ICosmosPostRepository
    {
        private readonly ICosmosStore<CosmosPost> _cosmosStore; 


        // ICosmosStore pozwala na pobieranie i manipulowanie danymi w bazie azure cosmos db
        public CosmosPostRepository(ICosmosStore<CosmosPost> cosmosStore)
        {
            _cosmosStore = cosmosStore;
        }
        public async Task<IEnumerable<CosmosPost>> GetAllAsync()
        {
            var posts = await _cosmosStore.Query().ToListAsync(); // querry to metoda cosmosa
            return posts;  
        } 
        public async Task<CosmosPost> GetByIdAsync(string id)
        {
            var post = await _cosmosStore.FindAsync(id); //findasync to metoda wewnetrza cosmosa 
            return post;
        }
        public async Task<CosmosPost> AddAsync(CosmosPost post)
        {
            post.Id = Guid.NewGuid().ToString();                   //przypisujemy najpierw nowy guid
            return await _cosmosStore.AddAsync(post);
            
        } 
        public async Task UpdateAsync(CosmosPost post)  
        {
            await _cosmosStore.UpdateAsync(post);
        }
        public  async Task DeleteAsync(CosmosPost post)
        {
            await _cosmosStore.RemoveAsync(post);
        }

    }
}
