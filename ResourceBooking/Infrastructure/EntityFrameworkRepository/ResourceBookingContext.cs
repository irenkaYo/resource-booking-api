using Domain.Models;
using Infrastructure.DTO;
using Infrastructure.EntityFrameworkRepository.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFrameworkRepository;

public class ResourceBookingContext : DbContext
{
    DbSet<User> Users { get; set; }
    DbSet<Resource> Resources { get; set; }
    DbSet<Booking> Bookings { get; set; }
    DbSet<Location> Locations { get; set; }
    DbSet<Category> Categories { get; set; }
    DbSet<Feature> Features { get; set; }
    DbSet<ResourceFeature> ResourceFeatures { get; set; }

    public ResourceBookingContext(DbContextOptions<ResourceBookingContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new ResourceConfiguration());
        modelBuilder.ApplyConfiguration(new BookingConfiguration());
    }
}