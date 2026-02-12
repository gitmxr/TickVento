using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TickVento.Domain.Entities;

namespace TickVento.Infrastructure.Persistence.Configurations
{
    public class SeatConfiguration : IEntityTypeConfiguration<Seat>
    {
        public void Configure(EntityTypeBuilder<Seat> builder)
        {
            // Map Entity to Seats Table
            builder.ToTable("Seats");

            // Primary Key
            builder.HasKey(s => s.Id);

            // SeatNumber Rules
            builder.Property(s => s.SeatNumber)
                .IsRequired()
                .HasMaxLength(15);

            // SeatNumber must be unique per Event
            builder.HasIndex(s => new { s.EventId, s.SeatNumber })
                .IsUnique();

            // Category
            builder.Property(s => s.Category)
                .IsRequired();

            // Status
            builder.Property(s => s.Status)
                .IsRequired();

            // EventId (Required)
            builder.Property(s => s.EventId)
                .IsRequired();

            // BookingId (Optional)
            builder.Property(s => s.BookingId)
                .IsRequired(false);

            // Relationship: Seat -> Event (Many-to-One)
            builder.HasOne(s => s.Event)
                .WithMany(e => e.Seats)
                .HasForeignKey(s => s.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relationship: Seat -> Booking (Many-to-One, Optional)
            builder.HasOne(s => s.Booking)
                .WithMany(b => b.Seats)
                .HasForeignKey(s => s.BookingId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
