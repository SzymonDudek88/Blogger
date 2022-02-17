using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
   public interface IDapperPostRepository
    {
        public Post GetAllAsync(  );


        public Post GetByIdAsync(int id);

        public Post AddAsync(Post post);
       public void UpdateAsync(Post post);
       public void  DeleteAsync(Post post);

    }
}
