using Microsoft.EntityFrameworkCore;
using Phlo.Api.Models;
using System.Threading.Tasks;

namespace Phlo.Api.Context
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Laptop" },
                new Product { Id = 2, Name = "Keyboard" },
                new Product { Id = 3, Name = "Monitor" },
                new Product { Id = 4, Name = "Mouse" },
                new Product { Id = 5, Name = "Webcam" }
                );

            modelBuilder.Entity<Order>()
                .HasOne(p => p.Product)
                .WithMany(b => b.Orders);
        }
        public async Task<int> SaveChanges()
        {
            return await base.SaveChangesAsync();
        }
    }
}
