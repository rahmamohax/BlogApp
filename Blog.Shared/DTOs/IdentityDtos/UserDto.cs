using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Shared.DTOs.IdentityDtos
{
    public record UserDto
    (
        string Email,
        string DisplayName,
        string Token
    );
}
