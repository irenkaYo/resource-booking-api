using Domain.Models;

namespace Infrastructure.InterfacesRepositories;

public interface IBookingRepository
{
    public Task<List<Booking>> GetBookingsByUserId(Guid userId);
    public Task<Booking>? GetBookingById(Guid bookingId);
    public Task CreateBooking(Booking booking);
    public Task UpdateBooking(Booking booking);
//метод для проверки доступности брони 
}