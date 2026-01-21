
using Blog.Shared.DTOs.CommentDtos;

namespace Blog.Shared.DTOs.PostDtos
{
    public class PostDetailsDto : PostDto
    {
        public ICollection<CommentDto> Comments { get; set; } = [];
    }
}
