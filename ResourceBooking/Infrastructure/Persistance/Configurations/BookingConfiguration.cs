using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configurations;

public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.HasIndex(a => a.StartTime).IsUnique();
        builder.HasIndex(a => a.EndTime).IsUnique();
        
        builder.Property(a => a.Status)
            .HasConversion<string>();
    }
}