namespace Blog.Domain.Entities
{
    public class Category: BaseEntity
    {
        public string Name { get; set; } = null!;
        public ICollection<Post> Posts { get; set; } = [];
    }
}