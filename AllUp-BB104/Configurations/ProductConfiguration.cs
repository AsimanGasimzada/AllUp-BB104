using AllUp_BB104.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AllUp_BB104.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(x => x.Name).IsRequired(true).HasMaxLength(100);
        builder.Property(x => x.Name).IsRequired(true).HasMaxLength(100);
        builder.Property(x => x.Description).IsRequired(true).HasMaxLength(100);


        builder.Property(x => x.Rating).IsRequired(true).HasDefaultValue(5);
        builder.Property(x => x.Price).IsRequired(true).HasPrecision(18, 2);

        builder.ToTable(t => t.HasCheckConstraint("CK_Products_Price_Constraint", " Price > 0"));
        builder.ToTable(t => t.HasCheckConstraint("CK_Products_Rating_Constraint", " Rating BETWEEN 0 AND 5"));


    }
}