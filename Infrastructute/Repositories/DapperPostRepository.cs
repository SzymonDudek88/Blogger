using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructute.Repositories
{
    class DapperPostRepository : IDapperPostRepository
    {
        public Task<Post> AddAsync(Post post)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Post post)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Post>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Post> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Post post)
        {
            throw new NotImplementedException();
        }
    }
}
