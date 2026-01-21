using Blog.Domain.Contracts;
using Blog.Domain.Entities;
using Blog.Service.Abstraction;
using Blog.Shared.DTOs.CategoryDtos;

namespace Blog.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            var cats = await _repository.GetAllAsync();
            if (cats is null) return Enumerable.Empty<CategoryDto>();
            return cats.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                CreatedAt = c.CreatedAt
            });
        }
        public async Task<CategoryDto?> CreateAsync(CreateCategoryDto categoryDto)
        {
            var cats = await _repository.GetAllAsync();
            if (cats.Any(x => x.Name == categoryDto.Name)) return null;
            return new CategoryDto
            {
                Name = categoryDto.Name,
                CreatedAt =  DateTime.Now
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var cat = await _repository.GetByIdAsync(id);
            if (cat is null) return false;
            var hasPosts = await _repository.HasPostsAsync(id);
            if (hasPosts) return false;
            return await _repository.DeleteAsync(cat);
        }
    }

}
