using Microsoft.EntityFrameworkCore;
using RefactorThisV2.Domain.Entities;

namespace RefactorThisV2.Persistence
{
    public class RefactorThisV2DbContext : DbContext
    {
        public RefactorThisV2DbContext(DbContextOptions<RefactorThisV2DbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductOption> ProductOptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<ProductOption>().ToTable("ProductOptions");
        }
    }
}