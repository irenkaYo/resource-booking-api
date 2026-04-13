using System.Data.Common;
using Domain.Models;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces.Repositories;

namespace Infrastructure.Repositories;

public class ResourceFeatureRepository : IResourceFeatureRepository
{
    private readonly ResourceBookingContext db;

    public ResourceFeatureRepository(ResourceBookingContext context)
    {
        db = context;
    }
    
    public async Task Add(ResourceFeature resourceFeature)
    {
        await db.ResourceFeatures.AddAsync(resourceFeature);
        await db.SaveChangesAsync();
    }

    public async Task<bool> Exists(Guid resourceId, Guid featureId)
    {
        return await db.ResourceFeatures
            .AnyAsync(rf => rf.ResourceId == resourceId && rf.FeatureId == featureId);
    }
}