using OrdersMicroservice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace OrdersMicroservice.Domain.Context
{
    public class DataContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductOrder> ProductsOrders { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasKey(b => b.Id);
            modelBuilder.Entity<Product>().Property(b => b.Name).IsRequired();
            modelBuilder.Entity<Product>().Property(b => b.Price).IsRequired();
            modelBuilder.Entity<Product>().Property(b => b.Components).IsRequired();

            modelBuilder.Entity<Order>().HasKey(b => b.Id);
            modelBuilder.Entity<Order>().Property(b => b.Price).IsRequired();
            modelBuilder.Entity<Order>().Property(b => b.Status).IsRequired();
            modelBuilder.Entity<Order>().Property(b => b.Address).IsRequired();

            modelBuilder.Entity<ProductOrder>().HasKey(b => b.Id);
            modelBuilder.Entity<ProductOrder>().HasOne(b => b.Product)
                .WithMany(a => a.Orders)
                .HasForeignKey("ProductId");
            modelBuilder.Entity<ProductOrder>().HasOne(b => b.Order)
                .WithMany(a => a.Products)
                .HasForeignKey("OrderId");

            base.OnModelCreating(modelBuilder);
        }
    }
}
