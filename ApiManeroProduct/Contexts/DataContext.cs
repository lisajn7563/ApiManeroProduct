using ApiManeroProduct.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiManeroProduct.Contexts
{
    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
    {
        public DbSet<ProductEntity> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductEntity>()
                .ToContainer("Products")
                .HasPartitionKey(x => x.PartitionKey);
        }
    }
}
