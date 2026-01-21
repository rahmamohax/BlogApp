using Blog.Domain.Contracts;
using Blog.Domain.Entities;
using Blog.Service.Abstraction;
using Blog.Shared.DTOs.PostDtos;


namespace Blog.Service
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _repository;

        public PostService(IPostRepository repository)
        {
            this._repository = repository;
        }

        public async Task<PostDto> CreateAsync(CreateOrUpdatePostDto postDto)
        {
            var postToCreate = new Post
            {
                Title = postDto.Title,
                Content = postDto.Content,
                CategoryId = postDto.CategoryId,
                CreatedAt = DateTime.UtcNow,
                Status = Status.Published
            };
            //var create = await _repository.AddAsync(postToCreate);

            //return postDto;
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PostDto>> GetAllPostsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<PostDto> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(int id, CreateOrUpdatePostDto postDto)
        {
            throw new NotImplementedException();
        }

        public Task PublishAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task ArchiveAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
