using Infrastructure.DTO;

namespace Service.Interfaces.Services;

public interface IBookingService
{
    public Task<List<BookingDto>> GetBookingsByUserId(Guid userId);
    public Task<BookingDto> GetBookingById(Guid bookingId);
    public Task<BookingDto> CreateBooking(CreateBookingDto dto);
    public Task CancelBooking(Guid bookingId, Guid userId);
    
    Task MarkExpiredBookingsAsCompleted();
}