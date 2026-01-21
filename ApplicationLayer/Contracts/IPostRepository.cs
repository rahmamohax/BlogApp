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
        Task<bool> AddAsync(Post post);
        Task<bool> Update(Post post);
        Task<bool> Delete(Post post);

        Task<Post?> GetByIdAsync(int id);
        Task<IEnumerable<Post>> GetAllAsync();


    }
}
