using Blog.Service.Abstraction;
using Blog.Shared.DTOs.CategoryDtos;
using Blog.Shared.DTOs.CommentDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Web.Controllers
{
    [Route("api")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("posts/{postId}/comments")]
        public async Task<ActionResult<CommentDto>> GetAll( [FromRoute] int postId)
        {
            var comments = await _commentService.GetAllAsync(postId);
            return Ok(comments);
        }

        [HttpPost("posts/{postId}/comments")]
        public async Task<ActionResult<CommentDto>> Add([FromRoute] int postId,CreateCommentDto dto)
        {
            var result = await _commentService.AddAsync(postId, dto);
            if (result is null) return NotFound("Post not found.");
            return Ok(result);
        }

        [HttpDelete("posts/comment/{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var comment = await _commentService.DeleteAsync(id);
            if (!comment) return NotFound();
            return Ok(comment);
        }

    }
}
