using Domain.Models;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces.Repositories;

namespace Infrastructure.Repositories;

public class FeatureRepository : IFeatureRepository
{
    private readonly ResourceBookingContext db;

    public FeatureRepository(ResourceBookingContext context)
    {
        db = context;
    }
    
    public async Task CreateFeature(Feature feature)
    {
        await db.Features.AddAsync(feature);
        await db.SaveChangesAsync();
    }

    public async Task<Feature?> GetFeatureById(Guid featureId)
    {
        Feature? feature = await db.Features.FirstOrDefaultAsync(f => f.Id == featureId);
        return feature;
    }
}