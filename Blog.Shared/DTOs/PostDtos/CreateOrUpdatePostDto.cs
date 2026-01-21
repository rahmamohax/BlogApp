using System.ComponentModel.DataAnnotations;

namespace Blog.Shared.DTOs.PostDtos
{
    public class CreateOrUpdatePostDto
    {
        [Required]
        [MinLength(4)]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;
        [Required]
        [StringLength(500)]
        public string Content { get; set; } = string.Empty;
        [Required]
        public int CategoryId { get; set; }


    }
}
