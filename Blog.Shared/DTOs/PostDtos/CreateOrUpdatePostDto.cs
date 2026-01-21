using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Shared.DTOs.PostDtos
{
    public class CreateOrUpdatePostDto
    {

        [Required]
        [MinLength(4)]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Content { get; set; } = string.Empty;
        public int CategoryId { get; set; }


    }
}
