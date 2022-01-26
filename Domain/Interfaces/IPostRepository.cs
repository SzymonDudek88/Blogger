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
          IEnumerable<Post> GetAll();

          Post GetById(int id);

          Post Add(Post post);
          void Update(Post post);
          void Delete(Post post);


    }
}
