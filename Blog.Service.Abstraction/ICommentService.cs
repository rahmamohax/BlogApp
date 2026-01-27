using Blog.Shared.CommonResult;
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
        Task<Result<IEnumerable<CommentDto>>> GetAllAsync(int postId);
        Task<CommentDto?> AddAsync(int postId, CreateCommentDto comment);
        Task<bool> DeleteAsync(int postId, int id);
    }
}
