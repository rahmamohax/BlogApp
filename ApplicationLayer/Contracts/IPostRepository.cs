using Blog.Domain.Entities;

namespace Blog.Domain.Contracts
{
    public interface IPostRepository
    {
        Task<bool> AddAsync(Post post);
        Task<bool> Update(Post post);
        Task<bool> Delete(Post post);

        Task<Post?> GetByIdAsync(int id);
        Task<IEnumerable<Post>> GetAllAsync();


    }
}
