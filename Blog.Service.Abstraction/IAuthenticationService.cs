

using Blog.Shared.ComminResult;
using Blog.Shared.DTOs.IdentityDtos;

namespace Blog.Service.Abstraction
{
    public interface IAuthenticationService
    {
        Task<Result<UserDto>> LoginAsync(LoginDto login);
        Task<Result<UserDto>> RegisterAsync(RegisterDto register);
    }
}
