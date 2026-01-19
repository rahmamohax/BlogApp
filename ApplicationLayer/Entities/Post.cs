using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Entities
{
    public enum Status
    {
        Draft=1, Published, Archived
    }
    public class Post : BaseEntity
    {
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public Status Status { get; set; } = Status.Draft;
        public ICollection<Comment> Comments { get; set; } = [];

    }
}
