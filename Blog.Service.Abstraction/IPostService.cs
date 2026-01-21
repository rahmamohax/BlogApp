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
        Task<PostDetailsDto?> GetByIdAsync(int id);

        Task<PostDto?> CreateAsync(CreateOrUpdatePostDto postDto);
        Task<bool> UpdateAsync(int id, CreateOrUpdatePostDto postDto);
        Task<bool> DeleteAsync(int id);

        Task<bool> PublishAsync(int id);
        Task<bool> ArchiveAsync(int id);


    }
}
