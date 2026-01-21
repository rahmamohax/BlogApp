using Blog.Domain.Contracts;
using Blog.Domain.Entities;
using Blog.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Persistence.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly BlogDbContext _dbContext;

        public CommentRepository(BlogDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task AddAsync(Comment comment)
        {
            await _dbContext.Comments.AddAsync(comment);
            await _dbContext.SaveChangesAsync();
        }

        public async void DeleteAsync(Comment comment)
        {
            _dbContext.Comments.Remove(comment);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Comment>> GetAllAsync(int postId)
        {
            return await _dbContext.Comments.Where(x=> x.PostId == postId).ToListAsync();
        }
    }
}
