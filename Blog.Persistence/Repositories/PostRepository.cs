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

        public async Task AddAsync(Post post)
        {
            await _dbContext.Posts.AddAsync(post);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(Post post)
        {
            _dbContext.Posts.Remove(post);
            await _dbContext.SaveChangesAsync();

        }

        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            return await _dbContext.Posts.ToListAsync();
        }

        public async Task<Post?> GetByIdAsync(int id)
        {
            return await _dbContext.Posts.FindAsync(id);
        }

        public async void Update(Post post)
        {
            _dbContext.Posts.Update(post);
            await _dbContext.SaveChangesAsync();

        }
    }
}
