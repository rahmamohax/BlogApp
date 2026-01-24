using Blog.Domain.Contracts;
using Blog.Domain.Entities;
using Blog.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Blog.Persistence.Data.DataSeed
{
    public class DataSeeder : IDataSeeder
    {
        private readonly BlogDbContext _dbContext;

        public DataSeeder(BlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task InitializeAsync()
        {
            try
            {
                var hasCat = await _dbContext.Categories.AnyAsync();
                var hasPosts = await _dbContext.Posts.AnyAsync();
                var hasComments = await _dbContext.Comments.AnyAsync();
                if (hasCat && hasPosts && hasComments) return;
                if (!hasCat) await SeedDataFromJsonAsync<Category>("categories.json", _dbContext.Categories);
                await _dbContext.SaveChangesAsync();

                if (!hasPosts) await SeedDataFromJsonAsync<Post>("posts.json", _dbContext.Posts);
                await _dbContext.SaveChangesAsync();

                if (!hasComments) await SeedDataFromJsonAsync<Comment>("comments.json", _dbContext.Comments);
                await _dbContext.SaveChangesAsync();


            }
            catch (Exception ex)
            {
                Console.WriteLine($"Data Seeding Failed : {ex}");
            }
        }

        private async Task SeedDataFromJsonAsync<T>(string fileName, DbSet<T> set) where T : BaseEntity
        {
            //C:\Users\rhmar\GitHub\BlogApi\Blog.Persistence\Data\DataSeed\JSONFiles\categories.json
            string filePath = @"..\Blog.Persistence\Data\DataSeed\JSONFiles\" + fileName;
            if(!File.Exists(filePath)) throw new FileNotFoundException($"File is not found {fileName}");
            try
            {
                var stream = File.OpenRead(filePath);
                var data =await JsonSerializer.DeserializeAsync<List<T>>(stream, new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new JsonStringEnumConverter() }
                });

                if (data != null) await set.AddRangeAsync(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error While Reading Json File : " + ex);
            }
        }
    }
}
