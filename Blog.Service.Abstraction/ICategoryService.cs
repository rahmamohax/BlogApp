using Blog.Shared.DTOs.CategoryDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.Abstraction
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllAsync();
        Task<CategoryDto?> CreateAsync(CreateCategoryDto categoryDto);
        Task<bool> DeleteAsync(int id);

    }
}
