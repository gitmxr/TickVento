using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TickVento.Domain.Entities;

namespace TickVento.Infrastructure.Persistence.Configurations;

public class EventSeatPriceConfiguration : IEntityTypeConfiguration<EventSeatPrice>
{
    public void Configure(EntityTypeBuilder<EventSeatPrice> builder)
    {
        // Map entity to EventSeatPrice Table 
        builder.ToTable("EventSeatPrices"); // Pluralize table name

        // Primary Key 
        builder.HasKey(e => e.Id);

        // EventId (FK)
        builder.Property(e => e.EventId)
            .IsRequired();

        // Category
        builder.Property(e => e.Category)
            .IsRequired();

        // Price 
        builder.Property(e => e.Price)
            .IsRequired()
            .HasPrecision(18, 2);

        // Relationship: Many EventSeatPrice to One Event
        builder.HasOne(e => e.Event)
            .WithMany(ev => ev.SeatPrices) 
            .HasForeignKey(e => e.EventId)
            .OnDelete(DeleteBehavior.Cascade);
    }

}
