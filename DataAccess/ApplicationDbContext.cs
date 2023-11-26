using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Entities.Models;
using System.Reflection.Emit;

namespace DataAccess
{
    public sealed class ApplicationDbContext : IdentityDbContext<Customer>
    {
        public DbSet<CartProduct> CartProducts { get; set; } = null!;
        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Feature> Features { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<ProductComment> ProductComments { get; set; } = null!;
        public DbSet<ProductImage> ProductImages { get; set; } = null!;
        public DbSet<ProductType> ProductTypes { get; set; } = null!;
        public DbSet<CustomerOrder> CustomerOrders { get; set; } = null!;
        public DbSet<PcBuild> PcBuilds { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder builder)
        {
            ProductType servicesType = new ProductType()
            {
                Id = int.MaxValue,
                Visible = false,
                Value = "Services",
                TypeImagepath = string.Empty
            };
            Product preBuildEntity = new Product()
            {
                Id = int.MaxValue,
                Name = "Pre-built PC",
                Description = "Our engineers will build your PC, so you can not worry about doing that by yourself",
                IsVisible = false,
                Quantity = int.MaxValue,
                Price = 0,
                ProductTypeId = servicesType.Id
            };
            base.OnModelCreating(builder);
            builder.Entity<ProductType>().HasData(servicesType);
            builder.Entity<Product>().HasData(preBuildEntity);
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}