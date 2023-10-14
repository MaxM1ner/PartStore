using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StoreUI.Models;

namespace StoreUI.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<CartProduct> CartProducts { get; set; } = null!;
        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Feature> Features { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<ProductComment> ProductComments { get; set; } = null!;
        public DbSet<ProductImage> ProductImages { get; set; } = null!;
        public DbSet<ProductType> ProductTypes { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}