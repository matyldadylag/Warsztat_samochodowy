using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Warsztat_samochodowy.Models;

namespace Warsztat_samochodowy.Data
{
    public class WorkshopDbContext : IdentityDbContext<ApplicationUserModel>
    {
        public WorkshopDbContext(DbContextOptions<WorkshopDbContext> options)
            : base(options)
        {
        }
        public DbSet<CustomerModel> Customers => Set<CustomerModel>();
        public DbSet<VehicleModel> Vehicles => Set<VehicleModel>();
        public DbSet<ServiceOrderModel> ServiceOrders => Set<ServiceOrderModel>();
        public DbSet<ServiceTaskModel> ServiceTasks => Set<ServiceTaskModel>();
        public DbSet<PartModel> Parts => Set<PartModel>();
        public DbSet<UsedPartModel> UsedParts => Set<UsedPartModel>();
        public DbSet<CommentModel> Comments => Set<CommentModel>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PartModel>()
                .Property(p => p.UnitPrice)
                .HasPrecision(18, 2);
        }
    }
}
