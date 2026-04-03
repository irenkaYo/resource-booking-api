using Domain.Models;
using Infrastructure.EntityFrameworkRepository;
using Infrastructure.InterfacesRepositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class BookingRepository : IBookingRepository
{
    private ResourceBookingContext db;
    
    public async Task<List<Booking>> GetBookingsByUserId(Guid userId)
    {
        List<Booking> bookings = await db.Bookings.Where(x => x.UserId == userId).ToListAsync();
        return bookings;
    }

    public async Task<Booking>? GetOneBookingById(Guid bookingId)
    {
        Booking? booking = await db.Bookings.FindAsync(bookingId);
        return booking;
    }

    public async Task CreateBooking(Booking booking)
    {
        await db.Bookings.AddAsync(booking);
        await db.SaveChangesAsync();
    }

    public async Task UpdateBooking(Booking booking)
    {
        db.Bookings.Update(booking);
        await db.SaveChangesAsync();
    }
}