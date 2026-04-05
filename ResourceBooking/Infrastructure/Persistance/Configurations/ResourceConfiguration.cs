using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configurations;

public class ResourceConfiguration : IEntityTypeConfiguration<Resource>
{
    public void Configure(EntityTypeBuilder<Resource> builder)
    {
        builder.Property(a => a.Name)
            .HasMaxLength(50)
            .IsRequired();
        
        builder.Property(a => a.Description)
            .HasMaxLength(250)
            .IsRequired();
    }
}