using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Persistence.Data.Configurations
{
    public class PostConfig : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.Property(x => x.Title).HasMaxLength(100);
            builder.Property(x => x.Content).HasMaxLength(500);
            builder.Property(x => x.Status).HasConversion<string>();
            builder.HasOne(x => x.Category).WithMany().HasForeignKey(x => x.CategoryId);
            builder.HasMany(x => x.Comments).WithOne().HasForeignKey(x => x.PostId);
        }
    }
}
