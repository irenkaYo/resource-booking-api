using Domain.Models;
using Infrastructure.EntityFrameworkRepository;
using Infrastructure.InterfacesRepositories;
using Microsoft.EntityFrameworkCore;

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
            .Include(x => x.Features)
            .ToListAsync();
        return resources;
    }

    public async Task<Resource>? GetResourceById(Guid resourceId)
    {
        Resource? resource = await db.Resources
            .Include(x => x.Features)
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
            .Include(r => r.Features)
            .AsQueryable();

        if (locationId.HasValue)
            query = query.Where(r => r.LocationId == locationId);

        if (capacity.HasValue)
            query = query.Where(r => r.Capacity >= capacity);

        if (categoryId.HasValue)
            query = query.Where(r => r.CategoryId == categoryId);

        if (featureId.HasValue)
            query = query.Where(r => r.Features.Any(f => f.Id == featureId));

        return await query.ToListAsync();
    }
}