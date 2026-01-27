using Blog.Domain.Contracts;
using Blog.Domain.Entities.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Persistence.Data.DataSeed
{
    public class IdentityDataSeeder : IIdentityDataSeeder
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<IdentityDataSeeder> _logger;

        public IdentityDataSeeder(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<IdentityDataSeeder> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        public async Task InitializerAsync()
        {
            try
            {
                if(!_roleManager.Roles.Any())
                {
                   await _roleManager.CreateAsync(new IdentityRole("User"));
                   await _roleManager.CreateAsync(new IdentityRole("Admin"));
                }
                if(!_userManager.Users.Any())
                {
                    var user1 = new ApplicationUser()
                    {
                        DisplayName = "Mohamed",
                        UserName = "MohamedTarek",
                        Email = "mohamed@gmail.com",
                        PhoneNumber = "1234567890"
                    };
                    var user2 = new ApplicationUser()
                    {
                        DisplayName = "Malak",
                        UserName = "SalmaTarek",
                        Email = "salma@gmail.com",
                        PhoneNumber = "1234567880"
                    };
                    await _userManager.CreateAsync(user1, "P@ssw0rd");
                    await _userManager.AddToRoleAsync(user1, "Admin");

                    await _userManager.CreateAsync(user2, "P@ssw0rd");
                    await _userManager.AddToRoleAsync(user2, "User");
                }

            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Error happened while seeding identity data");
            }

        }
    }
}
