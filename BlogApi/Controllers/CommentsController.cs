using Blog.Service.Abstraction;
using Blog.Shared.DTOs.CategoryDtos;
using Blog.Shared.DTOs.CommentDtos;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("post/{postId}/comments")]
        public async Task<ActionResult<CommentDto>> GetAll( [FromRoute] int postId)
        {
            var result = await _commentService.GetAllAsync(postId);
            if(!result.Success) 
                return NotFound(new { errors = result.Errors ?? (result.Error is null ? [] : new[] { result.Error }) });

            return Ok(result.Value);
        }

        [Authorize]
        [HttpPost("post/{postId}/comments")]
        public async Task<ActionResult<CommentDto>> Add([FromRoute] int postId,CreateCommentDto dto)
        {
            var result = await _commentService.AddAsync(postId, dto);
            if (result is null) return NotFound("Post not found.");
            return Ok(result);
        }

        [Authorize]
        [HttpDelete("post/{postId}/comment/{id}")]
        public async Task<ActionResult<bool>> Delete([FromRoute] int postId,int id)
        {
            var comment = await _commentService.DeleteAsync(postId, id);
            if (!comment) return NotFound("Can't Delete Comment, Comment is not Found");
            return Ok(comment);
        }

    }
}
