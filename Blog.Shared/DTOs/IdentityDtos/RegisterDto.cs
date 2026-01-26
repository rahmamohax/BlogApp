using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Shared.DTOs.IdentityDtos
{
    public record RegisterDto([EmailAddress]string Email,string Username, string Password, string DisplayName, [Phone]string PhoneNumber);
}
