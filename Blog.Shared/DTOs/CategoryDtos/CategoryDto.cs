
namespace Blog.Shared.DTOs.CategoryDtos
{
    public class CategoryDto : CreateCategoryDto
    {

        //public ICollection<PostDto>? Posts { get; set; }
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
