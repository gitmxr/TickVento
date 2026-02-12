using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TickVento.Domain.Entities;

namespace TickVento.Infrastructure.Persistence.Configurations
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            // Map Enity To Bookings Table 
            builder.ToTable("Bookings");

            // Primary Key
            builder.HasKey(b => b.Id);
            
            // BookingDate
            builder.Property(b => b.BookingDate)
                .IsRequired();

            // Rule For Status
            builder.Property(b => b.Status)
                .IsRequired();

            // Relationship to User 
            builder.HasOne(b => b.User)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relationship to Event 
            builder.HasOne(b => b.Event)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.EventId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relationship to Seats
            builder.HasMany(b => b.Seats)
                .WithOne(s => s.Booking)
                .HasForeignKey(s => s.BookingId)
                .OnDelete(DeleteBehavior.SetNull);

        }
    }
}
