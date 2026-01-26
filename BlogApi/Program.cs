
using Blog.Domain.Contracts;
using Blog.Persistence.Data.DataSeed;
using Blog.Persistence.DbContexts;
using Blog.Persistence.Repositories;
using Blog.Service;
using Blog.Service.Abstraction;
using BlogApi.Web.Extentions;
using Blog.Domain.Entities.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BlogApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<BlogDbContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("MainConnection"));
            });

            // Identity (required for UserManager/RoleManager in IdentityDataSeeder)
            builder.Services
                .AddIdentityCore<ApplicationUser>(options =>
                {
                    // Seeder uses "P@ssw0rd"
                    options.Password.RequiredLength = 6;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireDigit = false;
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<BlogDbContext>();
                //.AddDefaultTokenProviders();

            builder.Services.AddScoped<IPostRepository, PostRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<ICommentRepository, CommentRepository>();

            builder.Services.AddScoped<IPostService, PostService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<ICommentService, CommentService>();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

            builder.Services.AddScoped<IDataSeeder, DataSeeder>();
            builder.Services.AddScoped<IIdentityDataSeeder, IdentityDataSeeder>();

            var app = builder.Build();

            #region Data Seeding

            await app.MigrateDatabase();
            await app.SeedDatabaseAsync();
            await app.SeedIdentityDataAsync();
            
            #endregion

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
