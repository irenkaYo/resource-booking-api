using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configurations;

public class ResourceFeatureConfiguration : IEntityTypeConfiguration<ResourceFeature>
{
    public void Configure(EntityTypeBuilder<ResourceFeature> builder)
    {
        builder.HasKey(rf => new { rf.ResourceId, rf.FeatureId });
        
        builder
            .HasOne(rf => rf.Resource)
            .WithMany(r => r.ResourceFeatures)
            .HasForeignKey(rf => rf.ResourceId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(rf => rf.Feature)
            .WithMany(f => f.ResourceFeatures)
            .HasForeignKey(rf => rf.FeatureId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}