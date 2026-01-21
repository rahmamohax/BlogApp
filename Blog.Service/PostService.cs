using Blog.Domain.Contracts;
using Blog.Domain.Entities;
using Blog.Service.Abstraction;
using Blog.Shared.DTOs;
using Blog.Shared.DTOs.CommentDtos;
using Blog.Shared.DTOs.PostDtos;


namespace Blog.Service
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _repository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICommentRepository _commentRepository;

        public PostService(IPostRepository repository, ICategoryRepository categoryRepository, ICommentRepository commentRepository)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
            _commentRepository = commentRepository;
        }

        public async Task<PostDto?> CreateAsync(CreateOrUpdatePostDto postDto)
        {
            if (postDto is null)
            {
                return null;
            }

            var category = await _categoryRepository.GetByIdAsync(postDto.CategoryId);
            if (category is null)
            {
                return null;
            }

            var postToCreate = new Post
            {
                Title = postDto.Title,
                Content = postDto.Content,
                CategoryId = postDto.CategoryId,
                CreatedAt = DateTime.UtcNow,
                Status = Status.Draft
            };
            var created = await _repository.AddAsync(postToCreate);
            if (!created)
            {
                return null;
            }
            
            return new PostDto
            {
                Id = postToCreate.Id,
                Title = postToCreate.Title,
                CategoryId = postToCreate.CategoryId,
                CategoryName = category.Name,
                CreatedAt = postToCreate.CreatedAt,
                Content = postToCreate.Content,
                Status = postToCreate.Status.ToString()
            };

        }

        public async Task<bool> DeleteAsync(int id)
        {
            var post = await _repository.GetByIdAsync(id);
            if (post is null) return false;
            return await _repository.Delete(post);
        }

        public async Task<IEnumerable<PostDto>> GetAllPostsAsync()
        {
            var posts= await _repository.GetAllAsync();
            if (posts is null) return Enumerable.Empty<PostDto>();
            return posts.Select(x => new PostDto
            {
                Id = x.Id,
                Title = x.Title,
                CategoryId = x.CategoryId,
                CategoryName = x.Category?.Name ?? string.Empty,
                Content = x.Content,
                Status = x.Status.ToString(),
                CreatedAt = x.CreatedAt
            });            
        }

        public async Task<PostDetailsDto?> GetByIdAsync(int id)
        {
            var post = await _repository.GetByIdAsync(id);
            if (post is null) return null;

            return new PostDetailsDto
            {
                Id = post.Id,
                Title = post.Title,
                CategoryId = post.CategoryId,
                CategoryName = post.Category?.Name ?? string.Empty,
                Content = post.Content,
                Status = post.Status.ToString(),
                CreatedAt = post.CreatedAt,
                Comments = post.Comments?.Select(c => new CommentDto
                {
                    Id = c.Id,
                    Text = c.Text,
                    PostId = c.PostId
                }).ToList() ?? Enumerable.Empty<CommentDto>().ToList()
            };
        }

        public async Task<bool> UpdateAsync(int id, CreateOrUpdatePostDto postDto)
        {
            if (postDto is null) return false;

            var postToUpdate = await _repository.GetByIdAsync(id);
            if (postToUpdate is null || postToUpdate.Status == Status.Archived) return false;

            var category = await _categoryRepository.GetByIdAsync(postDto.CategoryId);
            if (category is null)
            {
                return false;
            }

            postToUpdate.Title = postDto.Title;
            postToUpdate.Content = postDto.Content;
            postToUpdate.CategoryId = postDto.CategoryId;

            return await _repository.Update(postToUpdate);
        }

        public async Task<bool> PublishAsync(int id)
        {
            var post = await _repository.GetByIdAsync(id);
            if (post is null) return false;
            if (post.Status == Status.Archived || post.Status == Status.Published) return false;
            post.Status = Status.Published;
            return await _repository.Update(post);
        }

        public async Task<bool> ArchiveAsync(int id)
        {
            var post = await _repository.GetByIdAsync(id);
            if (post is null || post.Status == Status.Archived) return false;
            post.Status = Status.Archived;
            return await _repository.Update(post);
        }
    }
}
