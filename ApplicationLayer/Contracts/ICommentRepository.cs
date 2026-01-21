using Blog.Domain.Entities;

namespace Blog.Domain.Contracts
{
    public interface ICommentRepository
    {
        Task<bool> AddAsync(Comment comment);
        Task<Comment?> GetByIdAsync(int id);
        Task<IEnumerable<Comment>> GetAllAsync(int postId);
        Task<bool> DeleteAsync(Comment comment);
    }
}
