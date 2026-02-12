using Microsoft.EntityFrameworkCore;
using TickVento.Domain.Entities;

namespace TickVento.Infrastructure.Persistence.Data
{
    public class TickVentoDbContext : DbContext
    {
        // Core Entities
        public DbSet<User> Users => Set<User>();
        public DbSet<Event> Events => Set<Event>();
        public DbSet<Venue> Venues => Set<Venue>();
        public DbSet<Seat> Seats => Set<Seat>();
        public DbSet<Booking> Bookings => Set<Booking>();
        public DbSet<Payment> Payments => Set<Payment>();

        public TickVentoDbContext(DbContextOptions<TickVentoDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply all IEntityTypeConfiguration<T> in this assembly

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TickVentoDbContext).Assembly);
        }


    }
}
