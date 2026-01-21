using Blog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Contracts
{
    public interface IPostRepository
    {
        Task AddAsync(Post post);
        void Update(Post post);
        void Delete(Post post);

        Task<Post?> GetByIdAsync(int id);
        Task<IEnumerable<Post>> GetAllAsync();


    }
}
