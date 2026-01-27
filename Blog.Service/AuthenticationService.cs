using Blog.Domain.Entities.IdentityModule;
using Blog.Service.Abstraction;
using Blog.Shared.CommonResult;
using Blog.Shared.DTOs.IdentityDtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Blog.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthenticationService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return false;
            return true;
        }

        public async Task<Result<UserDto>> GetUserByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return Result.Fail<UserDto>("User Not Found");
            return Result.Ok(new UserDto(email, user.DisplayName, await CreateTokenAsync(user)));
        }

        public async Task<Result<UserDto>> LoginAsync(LoginDto login)
        {
            if (login is null) return Result.Fail<UserDto>("Invalid request.");

            var user = await _userManager.FindByEmailAsync(login.Email);
            if (user == null) return Result.Fail<UserDto>("Invalid email or password.");

            var isPassValid = await _userManager.CheckPasswordAsync(user, login.Password);
            if (!isPassValid) return Result.Fail<UserDto>("Invalid email or password.");
            var token = await CreateTokenAsync(user);
            return Result.Ok(new UserDto(user.Email!, user.DisplayName, token));
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
            {
                await _userManager.AddToRoleAsync(user, "User");
                return Result.Ok(new UserDto(user.Email!, user.DisplayName, await CreateTokenAsync(user)));
            }

            var errors = result.Errors.Select(e => e.Description).ToList();
            return Result.Fail<UserDto>(errors); 
        }

        private async Task<string> CreateTokenAsync(ApplicationUser user)
        {
            var Claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                new Claim(JwtRegisteredClaimNames.Name, user.UserName!)
            };

            var Roles = await _userManager.GetRolesAsync(user);
            foreach (var role in Roles)
            {
                Claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var secretKey = _configuration["JWTOptions:SecretKey"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!));
            var Cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var Token = new JwtSecurityToken(
                issuer: _configuration["JWTOptions:Issuer"],
                audience: _configuration["JWTOptions:Audience"],
                expires: DateTime.UtcNow.AddDays(1),
                claims: Claims,
                signingCredentials: Cred
                );
            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
    }
}
