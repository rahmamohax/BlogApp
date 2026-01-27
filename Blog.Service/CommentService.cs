using Blog.Domain.Contracts;
using Blog.Domain.Entities;
using Blog.Service.Abstraction;
using Blog.Shared.CommonResult;
using Blog.Shared.DTOs.CommentDtos;


namespace Blog.Service
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _repository;
        private readonly IPostRepository _postRepository;

        public CommentService(ICommentRepository repository, IPostRepository postRepository)
        {
            _repository = repository;
            _postRepository = postRepository;
        }

        public async Task<CommentDto?> AddAsync(int postId, CreateCommentDto commentDto)
        {
            var post = await _postRepository.GetByIdAsync(postId);
            if (post is null || post.Status == Status.Archived) return null;
            var comment = new Comment
            {
                PostId = postId,
                Text = commentDto.Text,
                CreatedAt = DateTime.Now,
                Post = post,
            };
            var add = await _repository.AddAsync(comment);
            if(!add) return null;
            return new CommentDto
            {
                PostId =comment.PostId,
                Text = comment.Text,
                Id = comment.Id
            };
        }

        public async Task<bool> DeleteAsync(int postId, int id)
        {
            var post = await _postRepository.GetByIdAsync(postId);

            if (post is null) return false;

            if(post.Status == Status.Draft || post.Status == Status.Archived) return false;

            var comment = await _repository.GetByIdAsync(id);

            if (comment is null) return false;

            return await _repository.DeleteAsync(comment);
        }

        public async Task<Result<IEnumerable<CommentDto>>> GetAllAsync(int postId)
        {
            var post = await _postRepository.GetByIdAsync(postId);
            if (post is null) return Result.Fail<IEnumerable<CommentDto>>("post does not exist");

            if (post.Status == Status.Draft || post.Status == Status.Archived)
                return Result.Fail<IEnumerable<CommentDto>>($"post not found");


            var comments = await _repository.GetAllAsync(postId);
            if (comments is null)  return Result.Ok(Enumerable.Empty<CommentDto>());
            var returnComm = comments.Select(x=> new CommentDto
            {
                Id = x.Id,
                Text = x.Text,
            });

            return Result.Ok(returnComm);
        }
    }
}
