using Cleverbit.CodingTask.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Cleverbit.CodingTask.Data
{
    public class CodingTaskContext : DbContext
    {
        public CodingTaskContext(DbContextOptions<CodingTaskContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable(nameof(User));
            modelBuilder.Entity<Product>().ToTable(nameof(Product));

            var scoreBuilder = modelBuilder.Entity<Order>();

            scoreBuilder.HasOne(b => b.Product)
            .WithMany(a => a.Orders)
            .HasForeignKey(b => b.ProductId);

            scoreBuilder.HasOne(b => b.User)
            .WithMany(a => a.Orders)
            .HasForeignKey(b => b.UserId);
        }
    }
}
