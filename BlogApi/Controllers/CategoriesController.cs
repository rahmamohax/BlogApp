using Blog.Service.Abstraction;
using Blog.Shared.DTOs.CategoryDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<CategoryDto>> GetAll()
        {
            var result = await _categoryService.GetAllAsync();
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<CategoryDto>> Create(CreateCategoryDto dto)
        {
            var cat = await _categoryService.CreateAsync(dto);
            if (cat == null) return Conflict("Error, Can't Create this Category. Please make sure you have unique name");
            return Ok(cat);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var delete = await _categoryService.DeleteAsync(id);
            if (!delete) return Conflict("Category cannot be deleted because it is associated with existing posts.");
            return Ok(delete);
        }


    }
}
