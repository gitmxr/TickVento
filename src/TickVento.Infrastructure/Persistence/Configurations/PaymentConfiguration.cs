using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TickVento.Domain.Entities;

namespace TickVento.Infrastructure.Persistence.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            // Map Entity to Payments Table
            builder.ToTable("Payments");

            // Primary Key
            builder.HasKey(p => p.Id);

            // Amount
            builder.Property(p => p.Amount)
                .IsRequired();

            // Status
            builder.Property(p => p.Status)
                .IsRequired();

            // BookingId
            builder.Property(p => p.BookingId)
                .IsRequired();

            // Method
            builder.Property(p => p.Method)
                .IsRequired();

            // TransactionId (nullable)
            builder.Property(p => p.TransactionId)
                .IsRequired(false);

            // FailureReason (nullable)
            builder.Property(p => p.FailureReason)
                .IsRequired(false)
                .HasMaxLength(500);

            // Relationship To Booking
            builder.HasOne(p => p.Booking)
                .WithMany() 
                .HasForeignKey(p => p.BookingId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
