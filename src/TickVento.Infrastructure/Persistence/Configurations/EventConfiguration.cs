using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TickVento.Domain.Entities;


namespace TickVento.Infrastructure.Persistence.Configurations
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            // Map Entity to Events Table
            builder.ToTable("Events");

            // Primary Key 
            builder.HasKey(e => e.Id);

            // Rules For Title 
            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(200);

            // Rules For Description 
            builder.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(1000);

            // Rules For EventDate
            builder.Property(e => e.EventDate)
                .IsRequired();

            // Rule For Venue
            builder.HasOne(e => e.Venue)
                .WithMany()
                .HasForeignKey(e => e.VenueId)
                .OnDelete(DeleteBehavior.Restrict);

            // Rules For Seats
            builder.HasMany(e => e.Seats)
                .WithOne(s => s.Event)
                .HasForeignKey(s => s.EventId)
                .OnDelete(DeleteBehavior.Cascade);
            
            // Relationship to EventSeatPrice
            builder.HasMany(e => e.SeatPrices)
                   .WithOne(sp => sp.Event)
                   .HasForeignKey(sp => sp.EventId)
                   .OnDelete(DeleteBehavior.Cascade);


            // Index
            builder.HasIndex(e => e.EventDate);

        }
    }
}
