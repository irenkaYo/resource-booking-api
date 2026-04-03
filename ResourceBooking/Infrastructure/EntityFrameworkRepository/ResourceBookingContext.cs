using Domain.Models;
using Infrastructure.DTO;
using Infrastructure.EntityFrameworkRepository.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFrameworkRepository;

public class ResourceBookingContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Resource> Resources { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Feature> Features { get; set; }
    public DbSet<ResourceFeature> ResourceFeatures { get; set; }

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