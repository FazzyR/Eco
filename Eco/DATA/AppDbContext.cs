using Microsoft.EntityFrameworkCore;
using Eco.Models;
namespace Eco.DATA
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }


        public DbSet<Product> Products { get; set; }

        public DbSet<ChartItem> ChartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChartItem>()
                           .HasKey(ci => ci.Id); // Define the primary key for CartItem

            modelBuilder.Entity<ChartItem>()
                .Property(ci => ci.Id)
                .ValueGeneratedOnAdd(); // Configure Id to be auto-generated on add

            modelBuilder.Entity<ChartItem>()
                .Property(ci => ci.Quantity)
                .IsRequired(); // Quantity is required

            // Define the relationship between Product and CartItem
            modelBuilder.Entity<ChartItem>()
                .HasOne(ci => ci.Product)        // Each CartItem has one Product
                .WithMany(p => p.ChartItems)      // Each Product can have many CartItems
                .HasForeignKey(ci => ci.ProductId); // Define the foreign key property

            // Optionally, you can configure behavior for cascade delete
            modelBuilder.Entity<ChartItem>()
                .HasOne(ci => ci.Product)
                .WithMany(p => p.ChartItems)
                .HasForeignKey(ci => ci.ProductId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete if a product is deleted        }


        }
    }
}
