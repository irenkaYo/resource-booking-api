using Domain.Models;

namespace Service.Interfaces.Repositories;

public interface IBookingRepository
{
    public Task<List<Booking>> GetBookingsByUserId(Guid userId);
    public Task<Booking>? GetBookingById(Guid bookingId);
    public Task AddBooking(Booking booking);
    public Task UpdateBooking(Booking booking);
    public Task MarkExpiredBookingsAsCompleted();
    Task<bool> HasConflict(Guid resourceId, DateTime startTime, DateTime endTime);
}