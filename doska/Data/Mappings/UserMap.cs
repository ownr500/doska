using doska.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace doska.Data.Mappings;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(user => user.PictureId).IsRequired(false).HasDefaultValue(null);
        builder.HasOne<Picture>(user => user.Picture)
            .WithMany()
            .HasForeignKey(user => user.PictureId);
        // .OnDelete(DeleteBehavior.Restrict);
    }
}