using AllUp_BB104.Interceptors;
using AllUp_BB104.Models;
using AllUp_BB104.Models.Common;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AllUp_BB104.Contexts;

public class AppDbContext : IdentityDbContext<AppUser>
{
    private readonly BaseAuditableInterceptor _baseAuditableInterceptor;
    public AppDbContext(DbContextOptions options, BaseAuditableInterceptor baseAuditableInterceptor) : base(options)
    {
        _baseAuditableInterceptor = baseAuditableInterceptor;
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        modelBuilder.Entity<Product>().HasQueryFilter(x => !x.IsDeleted);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_baseAuditableInterceptor);
        base.OnConfiguring(optionsBuilder);
    }
    public required DbSet<Product> Products { get; set; }
    public required DbSet<ProductImage> ProductImages { get; set; }
    public required DbSet<ProductTag> ProductTags { get; set; }
    public required DbSet<Tag> Tags { get; set; }
    public required DbSet<Category> Categories { get; set; }
    public required DbSet<Brand> Brands { get; set; }
    public required DbSet<BasketItem> BasketItems { get; set; }
    public required DbSet<Setting> Settings { get; set; }
    public required DbSet<Comment> Comments { get; set; }
}
