using AllUp_BB104.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AllUp_BB104.Contexts;

public class AppDbContext : IdentityDbContext<AppUser>
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
    public required DbSet<Product> Products { get; set; }
    public required DbSet<ProductImage> ProductImages { get; set; }
    public required DbSet<ProductTag> ProductTags { get; set; }
    public required DbSet<Tag> Tags { get; set; }
    public required DbSet<Category> Categories { get; set; }
    public required DbSet<Brand> Brands { get; set; }
    public required DbSet<BasketItem> BasketItems { get; set; }
    public required DbSet<Setting> Settings{ get; set; }
}
