using Blog.Domain.Entities.IdentityModule;
using Blog.Service.Abstraction;
using Blog.Shared.ComminResult;
using Blog.Shared.DTOs.IdentityDtos;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthenticationService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<Result<UserDto>> LoginAsync(LoginDto login)
        {
            if (login is null) return Result.Fail<UserDto>("Invalid request.");

            var user = await _userManager.FindByEmailAsync(login.Email);
            if (user == null) return Result.Fail<UserDto>("Invalid email or password.");

            var isPassValid = await _userManager.CheckPasswordAsync(user, login.Password);
            if (!isPassValid) return Result.Fail<UserDto>("Invalid email or password.");

            return Result.Ok(new UserDto(user.Email!, user.DisplayName, "Token"));
        }

        public async Task<Result<UserDto>> RegisterAsync(RegisterDto register)
        {
            if (register is null) return Result.Fail<UserDto>("Invalid request.");

            var doesExist = await _userManager.FindByEmailAsync(register.Email);
            if (doesExist is not null)
                return Result.Fail<UserDto>($"User with email '{register.Email}' already exists.");

            var user = new ApplicationUser
            {
                Email = register.Email,
                DisplayName = register.DisplayName,
                PhoneNumber = register.PhoneNumber,
                UserName = register.Username,
            };

            var result = await _userManager.CreateAsync(user, register.Password);
            if (result.Succeeded)
                return Result.Ok(new UserDto(user.Email!, user.DisplayName, "Token"));

            var errors = result.Errors.Select(e => e.Description).ToList();
            return Result.Fail<UserDto>(errors); 
        }
    }
}
