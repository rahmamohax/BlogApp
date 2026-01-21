using Blog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Contracts
{
    public interface ICategoryRepository
    {
        Task<bool> CreateAsync(Category category);
        Task<IEnumerable<Category>> GetAllAsync();
        Task<bool> DeleteAsync(Category category);
        Task<Category?> GetByIdAsync(int id);

        //Task<bool> HasPostsAsync(int categoryId); // will be a helper method in service implementation

    }
}
