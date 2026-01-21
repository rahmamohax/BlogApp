using System.ComponentModel.DataAnnotations;


namespace Blog.Shared.DTOs.CommentDtos
{
    public class CommentDto
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        public string Text { get; set; } = string.Empty;
        public int PostId { get; set; }
    }
}
