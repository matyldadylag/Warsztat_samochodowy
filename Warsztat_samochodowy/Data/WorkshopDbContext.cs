using Microsoft.EntityFrameworkCore;
using Warsztat_samochodowy.Models;

namespace Warsztat_samochodowy.Data
{
    public class WorkshopDbContext : DbContext
    {
        public WorkshopDbContext(DbContextOptions<WorkshopDbContext> options)
            : base(options)
        {
        }
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Vehicle> Vehicles => Set<Vehicle>();
        public DbSet<ServiceOrder> ServiceOrders => Set<ServiceOrder>();
        public DbSet<ServiceTask> ServiceTasks => Set<ServiceTask>();
        public DbSet<Part> Parts => Set<Part>();
        public DbSet<UsedPart> UsedParts => Set<UsedPart>();
        public DbSet<Comment> Comments => Set<Comment>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Part>()
                .Property(p => p.UnitPrice)
                .HasPrecision(18, 2); // or (10, 2) depending on your needs
        }
    }
}
