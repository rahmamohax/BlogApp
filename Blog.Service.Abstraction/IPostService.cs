using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Shared.DTOs.PostDtos;

namespace Blog.Service.Abstraction
{
    public interface IPostService
    {
        Task<IEnumerable<PostDto>> GetAllPostsAsync();
        Task<PostDto> GetByIdAsync(int id);

        Task<PostDto> CreateAsync(CreateOrUpdatePostDto postDto);
        Task UpdateAsync(int id, CreateOrUpdatePostDto postDto);
        Task DeleteAsync(int id);

        Task PublishAsync(int id);
        Task ArchiveAsync(int id);


    }
}
