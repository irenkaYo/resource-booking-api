using Domain.Models;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;
using Service.Interfaces.Repositories;

namespace Infrastructure.Repositories;

public class BookingRepository : IBookingRepository
{
    private readonly ResourceBookingContext db;

    public BookingRepository(ResourceBookingContext context)
    {
        db = context;
    }
    
    public async Task<List<Booking>> GetBookingsByUserId(Guid userId)
    {
        List<Booking> bookings = await db.Bookings
            .Include(x => x.Resource)
            .Where(x => x.UserId == userId)
            .ToListAsync();
        return bookings;
    }

    public async Task<Booking>? GetBookingById(Guid bookingId)
    {
        Booking? booking = await db.Bookings
            .Include(x => x.Resource)
            .FirstOrDefaultAsync(x => x.Id == bookingId);
        return booking;
    }

    public async Task AddBooking(Booking booking)
    {
        await db.Bookings.AddAsync(booking);
    }

    public async Task UpdateBooking(Booking booking)
    {
        db.Bookings.Update(booking);
        await db.SaveChangesAsync();
    }
    
    public async Task MarkExpiredAsCompleted(DateTimeOffset now)
    {
        await db.Bookings
            .Where(b =>
                b.EndTime <= now &&
                b.Status != BookingStatus.Completed &&
                b.Status != BookingStatus.Canceled)
            .ExecuteUpdateAsync(b => b
                .SetProperty(x => x.Status, BookingStatus.Completed));
    }
    
    public async Task<bool> HasConflict(Guid resourceId, DateTimeOffset startTime, DateTimeOffset endTime)
    {
        return await db.Bookings
            .AnyAsync(b =>
                b.ResourceId == resourceId &&
                b.Status != BookingStatus.Canceled &&
                b.StartTime < endTime &&
                b.EndTime > startTime);
    }

    public async Task<List<Booking>> GetBookingsByResourceId(Guid resourceId)
    {
        List<Booking> bookings = await db.Bookings
            .Include(x => x.Resource)
            .Where(x => x.ResourceId == resourceId)
            .ToListAsync();
        return bookings;
    }
}