using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
   public interface IPostRepository //dotyczy skladowania - okresla co trzeba uzywac odnosnie skladowania//
        // jest to jedne ze skladowan 
    {
        Task<IEnumerable<Post>> GetAllAsync();
        Task<IEnumerable<Post>> GetPostByTitleContentAsync(string content);

          Task<Post> GetByIdAsync(int id);

         Task<Post> AddAsync(Post post);
          Task UpdateAsync(Post post);
          Task DeleteAsync(Post post);


    }
}
