using Domain.Models;
using Infrastructure.EntityFrameworkRepository;
using Infrastructure.InterfacesRepositories;
using Microsoft.EntityFrameworkCore;

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
}