using Domain.Models;
using Infrastructure.EntityFrameworkRepository;
using Infrastructure.InterfacesRepositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ResourceRepository : IResourceRepository
{
    private ResourceBookingContext db;
    
    public async Task<List<Resource>> GetAllResources()
    {
        List<Resource> resources = await db.Resources.ToListAsync();
        return resources;
    }

    public async Task<Resource>? GetResourceById(Guid resourceId)
    {
        Resource? resource = await db.Resources.FindAsync(resourceId);
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
}