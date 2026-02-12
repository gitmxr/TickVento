using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TickVento.Domain.Entities;

namespace TickVento.Infrastructure.Persistence.Configurations;
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // Map entity to Users table
        builder.ToTable("Users");

        // Primary key is Id
        builder.HasKey(u => u.Id);

        // Rules for Email 
        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(256);
        
        // email must be unique 
        builder.HasIndex(u => u.Email)
            .IsUnique();
        
        // Rules of FullName
        builder.Property(u => u.FullName)
            .IsRequired()
            .HasMaxLength(100);

        // Rules for role
        builder.Property(u => u.Role)
            .IsRequired();

        // Rules for createAt
        builder.Property(u => u.CreatedAt)
            .IsRequired();

    }
}
