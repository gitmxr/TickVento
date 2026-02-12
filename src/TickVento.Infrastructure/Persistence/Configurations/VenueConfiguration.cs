using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TickVento.Domain.Entities;

namespace TickVento.Infrastructure.Persistence.Configurations;

public class VenueConfiguration : IEntityTypeConfiguration<Venue>
{
    
    public void Configure(EntityTypeBuilder<Venue> builder)
    {
        // Map entity to Venues table and add check constraint
        builder.ToTable("Venues", v =>
        {
            v.HasCheckConstraint("CK_Venue_Capacity", "[Capacity] > 0");
        });

        // Primary Key
        builder.HasKey(v => v.Id);

        // Rules for Name
        builder.Property(v => v.Name)
            .IsRequired()
            .HasMaxLength(200);

        // Rules for Capacity 
        builder.Property(v => v.Capacity)
            .IsRequired();

    }
}
