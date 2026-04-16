using Domain.Models;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces.Repositories;

namespace Infrastructure.Repositories;

public class ResourceRepository : IResourceRepository
{
    private readonly ResourceBookingContext db;

    public ResourceRepository(ResourceBookingContext context)
    {
        db = context;
    }
    
    public async Task<List<Resource>> GetAllResources()
    {
        List<Resource> resources = await db.Resources
            .Include(x => x.ResourceFeatures)
            .ThenInclude(rf => rf.Feature)
            .Include(x => x.Category)
            .Include(x => x.Location)
            .ToListAsync();
        return resources;
    }

    public async Task<Resource>? GetResourceById(Guid resourceId)
    {
        Resource? resource = await db.Resources
            .Include(x => x.ResourceFeatures)
            .ThenInclude(rf => rf.Feature)
            .Include(x => x.Category)
            .Include(x => x.Location)
            .FirstOrDefaultAsync(x  => x.Id == resourceId);
        return resource;
    }

    public async Task CreateResource(Resource resource)
    {
        await db.Resources.AddAsync(resource);
        await db.SaveChangesAsync();
    }
    
    public async Task UpdateResource(Resource resource)
    {
        db.Resources.Update(resource);
        await db.SaveChangesAsync();
    }
    
    public void SetXmin(Resource resource, uint xmin)
    {
        db.Entry(resource).Property("xmin").OriginalValue = xmin;
    }

    public async Task DeleteResource(Resource resource)
    {
        db.Resources.Remove(resource);
        await db.SaveChangesAsync();
    }
    
    public async Task<IEnumerable<Resource>> GetFilteredResources(Guid? locationId,
        Guid? categoryId,
        int? capacity,
        Guid? featureId)
    {
        var query = db.Resources
            .Include(r => r.ResourceFeatures)
            .ThenInclude(rf => rf.Feature)
            .AsQueryable();

        if (locationId.HasValue)
            query = query.Where(r => r.LocationId == locationId);

        if (capacity.HasValue)
            query = query.Where(r => r.Capacity >= capacity);

        if (categoryId.HasValue)
            query = query.Where(r => r.CategoryId == categoryId);

        if (featureId.HasValue)
            query = query.Where(r => r.ResourceFeatures
                .Any(rf => rf.FeatureId == featureId));

        return await query.ToListAsync();
    }
}