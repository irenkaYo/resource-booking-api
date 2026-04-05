using Domain.Models;
using Infrastructure.DTO;
using Infrastructure.EntityFrameworkRepository;
using Infrastructure.InterfacesRepositories;
using Microsoft.EntityFrameworkCore;

namespace Service.Services;

public class BookingService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IUserRepository _userRepository;
    private readonly ResourceBookingContext _context;
    
    public BookingService(IBookingRepository bookingRepository, IUserRepository userRepository, ResourceBookingContext context)
    {
        _bookingRepository = bookingRepository;
        _userRepository = userRepository;
        _context = context;
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
        await using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            if (dto.EndTime <= dto.StartTime)
                throw new Exception("Invalid booking time");
            
            var hasConflict = await _context.Bookings
                .AnyAsync(b =>
                    b.ResourceId == dto.ResourceId &&
                    b.StartTime < dto.EndTime &&
                    b.EndTime > dto.StartTime);
            
            if (hasConflict)
                throw new Exception("Time slot is already booked");
            
            Booking booking = new Booking(dto.ResourceId, dto.UserId, dto.StartTime, dto.EndTime);
            await _bookingRepository.AddBooking(booking);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            return ConvertBookingToBookingDto(booking);
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
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