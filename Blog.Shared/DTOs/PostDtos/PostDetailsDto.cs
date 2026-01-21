
using Blog.Shared.DTOs.CommentDtos;

namespace Blog.Shared.DTOs.PostDtos
{
    public class PostDetailsDto : PostDto
    {
        public List<CommentDto>? Comments { get; set; }
    }
}
