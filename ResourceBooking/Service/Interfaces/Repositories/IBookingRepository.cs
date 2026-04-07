using Domain.Models;

namespace Service.Interfaces.Repositories;

public interface IBookingRepository
{
    public Task<List<Booking>> GetBookingsByUserId(Guid userId);
    public Task<Booking>? GetBookingById(Guid bookingId);
    public Task AddBooking(Booking booking);
    public Task UpdateBooking(Booking booking);
    public Task<List<Booking>> GetExpiredBookings(DateTime now);
    Task<bool> HasConflict(Guid resourceId, DateTime startTime, DateTime endTime);
    public Task<List<Booking>> GetBookingsByResourceId(Guid resourceId);
}