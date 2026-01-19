namespace Blog.Domain.Entities
{
    public class Comment:BaseEntity
    {
        public string Text { get; set; } = null!;
        public int PostId { get; set; }
        public Post Post { get; set; } = null!;

    }
}