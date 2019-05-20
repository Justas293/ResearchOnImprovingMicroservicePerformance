using Microsoft.EntityFrameworkCore;
using ProductServiceAPI.Models;
using System;

namespace ProductServiceAPI.DbContexts
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name = "Electronics",
                    Description = "Electronic Items",
                },
                new Category
                {
                    Id = 2,
                    Name = "Clothes",
                    Description = "Dresses",
                },
                new Category
                {
                    Id = 3,
                    Name = "Grocery",
                    Description = "Grocery Items",
                }
            );
            for (int i = 0; i < 100; i++)
            {
                modelBuilder.Entity<Product>().HasData(
                    new Product
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = $"Product_{i}",
                        Description = $"Product_{i} description",
                        CategoryId = (i % 3) + 1,
                        Price = i + 10
                    });
            }
        }

    }
}