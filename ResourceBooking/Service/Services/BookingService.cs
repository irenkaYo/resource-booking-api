using Domain.Models;
using Infrastructure.DTO;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces.Persistance;
using Service.Interfaces.Repositories;
using Service.Interfaces.Services;

namespace Service.Services;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IUserRepository _userRepository;
    private readonly IResourceRepository _resourceRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public BookingService(
        IBookingRepository bookingRepository, 
        IUserRepository userRepository, 
        IResourceRepository resourceRepository, 
        IUnitOfWork unitOfWork)
    {
        _bookingRepository = bookingRepository;
        _userRepository = userRepository;
        _resourceRepository = resourceRepository;
        _unitOfWork = unitOfWork;
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
        await _unitOfWork.BeginTransactionAsync();

        try
        {
            if (dto.EndTime <= dto.StartTime)
                throw new Exception("Invalid booking time");
            
            Resource resource = await GetResource(dto.ResourceId);
            if (!resource.IsActive)
                throw new Exception("Resource is not active");

            var hasConflict = await _bookingRepository.HasConflict(
                dto.ResourceId,
                dto.StartTime,
                dto.EndTime);

            if (hasConflict)
                throw new Exception("Time slot is already booked");

            var booking = new Booking(dto.ResourceId, dto.UserId, dto.StartTime, dto.EndTime);

            await _bookingRepository.AddBooking(booking);

            await _unitOfWork.CommitAsync();

            return ConvertBookingToBookingDto(booking);
        }
        catch (DbUpdateException)
        {
            await _unitOfWork.RollbackAsync();
            throw new Exception("Time slot is already booked");
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }

    public async Task CancelBooking(Guid bookingId, Guid userId)
    {
        User user = await GetUser(userId);
        bool hasBooking = user.Bookings.Any(x => x.Id == bookingId);
        if (!hasBooking && user.Role != UserRole.Admin)
            throw new Exception("You cannot cancel booking");
        
        Booking booking = await GetBooking(bookingId);
        booking.Status = BookingStatus.Canceled;
        await _bookingRepository.UpdateBooking(booking);
    }
    
    public async Task MarkExpiredBookingsAsCompleted()
    {
        DateTime now = DateTime.UtcNow;
        List<Booking> bookings = await _bookingRepository.GetExpiredBookings(now);

        foreach (var booking in bookings)
        {
            booking.Status = BookingStatus.Completed;
            await _bookingRepository.UpdateBooking(booking);
        }
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

    private async Task<Resource> GetResource(Guid resourceId)
    {
        Resource? resource = await _resourceRepository.GetResourceById(resourceId);
        if (resource == null)
            throw new Exception("Resource not found");
        return resource;
    }
    
    private async Task<User> GetUser(Guid userId)
    {
        var user = await _userRepository.GetUserById(userId);

        if (user == null)
            throw new Exception("User not found");
        
        return user;
    }
}