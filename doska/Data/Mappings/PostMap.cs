using doska.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace doska.Data.Mappings;

public class PostMap : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.HasKey(post => post.Id);
        builder.Property(post => post.Id).HasDefaultValueSql("newsequentialid()");
        builder.Property(post => post.Title).IsRequired();
        builder.Property(post => post.Content).IsRequired();
        builder.Property(post => post.Price).IsRequired();
        builder.HasOne(post => post.User)
            .WithMany(user => user.Posts)
            .HasForeignKey(post => post.UserId)
            .OnDelete(DeleteBehavior.SetNull);
        builder.HasMany<Picture>(post => post.Pictures)
            .WithMany(picture => picture.Posts);

        builder.Property(post => post.UserId)
            .IsRequired(false);
    }
}