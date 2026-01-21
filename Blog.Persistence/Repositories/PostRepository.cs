using Blog.Domain.Contracts;
using Blog.Domain.Entities;
using Blog.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Blog.Persistence.Repositories
{
    public class PostRepository :  IPostRepository
    {
        private readonly BlogDbContext _dbContext;

        public PostRepository(BlogDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddAsync(Post post)
        {
            await _dbContext.Posts.AddAsync(post);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> Delete(Post post)
        {
            _dbContext.Posts.Remove(post);
            return await _dbContext.SaveChangesAsync() > 0;

        }

        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            return await _dbContext.Posts.ToListAsync();
        }

        public async Task<Post?> GetByIdAsync(int id)
        {
            return await _dbContext.Posts.FindAsync(id);
        }

        public async Task<bool> Update(Post post)
        {
            _dbContext.Posts.Update(post);
           return await _dbContext.SaveChangesAsync() > 0;

        }
    }
}
