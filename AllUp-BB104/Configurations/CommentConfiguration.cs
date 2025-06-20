using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AllUp_BB104.Configurations;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.Property(x => x.Text).IsRequired(true).HasMaxLength(500);
        builder.Property(x => x.Rating).IsRequired(true).HasDefaultValue(5);
        builder.ToTable(t => t.HasCheckConstraint("CK_Comments_Rating_Constraint", " Rating BETWEEN 0 AND 5"));
    }
}