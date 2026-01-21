using Blog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Contracts
{
    public interface ICommentRepository
    {
        Task<bool> AddAsync(Comment comment);
        Task<IEnumerable<Comment>> GetAllAsync(int postId);
        Task<bool> DeleteAsync(Comment comment);
    }
}
