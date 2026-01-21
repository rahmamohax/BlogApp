
using System.ComponentModel.DataAnnotations;

namespace Blog.Shared.DTOs.CategoryDtos
{
    public class CreateCategoryDto
    {
        [Required, MinLength(3)]
        public string Name { get; set; } = string.Empty;
    }
}
