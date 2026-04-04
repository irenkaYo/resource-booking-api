using Domain.Models;
using Infrastructure.DTO;
using Infrastructure.InterfacesRepositories;

namespace Service.Services;

public class BookingService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IUserRepository _userRepository;
    
    public BookingService(IBookingRepository bookingRepository, IUserRepository userRepository)
    {
        _bookingRepository = bookingRepository;
        _userRepository = userRepository;
    }
    
    public async Task<List<BookingDto>> GetBookingsByUserId(Guid userId)
    {
        List<Booking> bookings = await _bookingRepository.GetBookingsByUserId(userId);
        List<BookingDto> bookingDtos = new List<BookingDto>();
        foreach (Booking booking in bookings)
        {
            BookingDto bookingDto = ConvertBookingToBookingDto(booking);
            bookingDtos.Add(bookingDto);
        }
        return bookingDtos;
    }

    public async Task<BookingDto> GetBookingById(Guid bookingId)
    {
        Booking booking = await GetBooking(bookingId);
        return ConvertBookingToBookingDto(booking);
    }

    public async Task<BookingDto> CreateBooking(CreateBookingDto dto)
    {
        if (dto.EndTime <= dto.StartTime)
            throw new Exception("Invalid booking time");
        
        Booking booking = new Booking(dto.ResourceId, dto.UserId, dto.StartTime, dto.EndTime);
        await _bookingRepository.CreateBooking(booking);
        return ConvertBookingToBookingDto(booking);
    }

    public async Task CancelBooking(Guid bookingId, Guid userId)
    {
        await GetAdminUser(userId);
        Booking booking = await GetBooking(bookingId);
        booking.Status = BookingStatus.Canceled;
        await _bookingRepository.UpdateBooking(booking);
    }
    
    private BookingDto ConvertBookingToBookingDto(Booking booking)
    {
        BookingDto dto = new BookingDto(
            booking.Id, 
            booking.ResourceId, 
            booking.UserId, 
            booking.StartTime, 
            booking.EndTime, 
            booking.Status, 
            booking.CreatedAt);
        return dto;
    }

    private async Task<Booking> GetBooking(Guid bookingId)
    {
        Booking? booking = await _bookingRepository.GetBookingById(bookingId);
        if (booking == null)
            throw new Exception("Booking not found");
        return booking;
    }
    
    private async Task GetAdminUser(Guid userId)
    {
        var user = await _userRepository.GetUserById(userId);

        if (user == null)
            throw new Exception("User not found");

        if (user.Role != UserRole.Admin)
            throw new Exception("Access denied");
    }
}