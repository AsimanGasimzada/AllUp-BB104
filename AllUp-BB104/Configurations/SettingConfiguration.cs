using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AllUp_BB104.Configurations;

public class SettingConfiguration : IEntityTypeConfiguration<Setting>
{
    public void Configure(EntityTypeBuilder<Setting> builder)
    {
        builder.Property(x => x.Key).IsRequired().HasMaxLength(64);
        builder.Property(x => x.Value).IsRequired().HasMaxLength(256);

        builder.HasIndex(x => x.Key).IsUnique();
    }
}
