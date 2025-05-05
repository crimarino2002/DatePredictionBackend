using Microsoft.EntityFrameworkCore;
using Sales_Date_Prediction.Domain.Entities;
using System.Reflection.Emit;

namespace Sales_Date_Prediction.Domain.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Shipper>Shippers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            base.OnModelCreating(mb);
            mb.Entity<Order>(entity =>
            {
                entity.HasNoKey();
                entity.ToView(null);
            });
            mb.Entity<Customer>(entity =>
            {
                entity.HasNoKey();
                entity.ToView(null);
            });
            mb.Entity<Employee>(entity =>
            {
                entity.HasNoKey();
                entity.ToView(null);
            });
            mb.Entity<Shipper>(entity =>
            {
                entity.HasNoKey();
                entity.ToView(null);
            });
            mb.Entity<Product>(entity =>
            {
                entity.HasNoKey();
                entity.ToView(null);
            });
           
        }
    }
}
