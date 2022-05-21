using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FlyBuy.Models;

namespace FlyBuy.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Rating> Ratings { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<AgeCategory> AgeCategories { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }

    }
}