namespace Service.Interfaces.Services;

public interface IBookingService
{
    Task MarkExpiredBookingsAsCompleted();
}