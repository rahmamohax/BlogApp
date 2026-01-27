using Blog.Service.Abstraction;
using Blog.Shared.DTOs.PostDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<ActionResult<PostDto>> Create(CreateOrUpdatePostDto postDto)
        {
            var result = await _postService.CreateAsync(postDto);
            if (result == null) return BadRequest("Unable to create Post");
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{postId}")]
        public async Task<ActionResult> Update(int postId, CreateOrUpdatePostDto postDto)
        {
            var result = await _postService.UpdateAsync(postId, postDto);
            if (!result) return BadRequest("Bad request, Unable to update post");
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<PostDto>> GetAll()
        {
            var result = await _postService.GetAllPostsAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PostDto>> Get(int id)
        {
            var result = await _postService.GetByIdAsync(id);
            if (result == null) return NotFound($"Can't find post with Id = {id}");
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}/publish")]
        public async Task<ActionResult> Publish(int id)
        {
            var published = await _postService.PublishAsync(id);
            if(!published) return UnprocessableEntity("Cannot publish an archived or non-existent post.");
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}/archive")]
        public async Task<ActionResult> Archive(int id)
        {
            var archived = await _postService.ArchiveAsync(id);
            if (!archived) return UnprocessableEntity("Post is already archived or does not exist."); 
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var deleted = await _postService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }

    }
}
