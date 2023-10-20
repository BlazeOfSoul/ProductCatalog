using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.API.Data.Entities.Categories;
using ProductCatalog.API.Data.Entities.Products;
using ProductCatalog.API.Data.Entities.Users;

namespace ProductCatalog.API.Data;

public class ApplicationContext : IdentityDbContext<User>
{
    public DbSet<Product> Products { get; set; }

    public DbSet<Category> Categories { get; set; }

    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithOne()
            .HasForeignKey<Product>(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}