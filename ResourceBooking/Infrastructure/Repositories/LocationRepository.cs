using Domain.Models;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces.Repositories;

namespace Infrastructure.Repositories;

public class LocationRepository : ILocationRepository
{
    private readonly ResourceBookingContext db;

    public LocationRepository(ResourceBookingContext context)
    {
        db = context;
    }
    
    public async Task CreateLocation(Location location)
    {
        await db.Locations.AddAsync(location);
        await db.SaveChangesAsync();
    }

    public async Task<Location?> GetLocationById(Guid locationId)
    {
        Location? location = await db.Locations.FirstOrDefaultAsync(l => l.Id == locationId);
        return location;
    }
}