using Blog.Shared.DTOs.CommentDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.Abstraction
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentDto>> GetAllAsync(int postId);
        Task<bool> AddAsync(int postId, CreateCommentDto comment);
        Task<bool> DeleteAsync(int id);
    }
}
