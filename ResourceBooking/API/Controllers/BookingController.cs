using Infrastructure.DTO;
using Microsoft.AspNetCore.Mvc;
using Service.Services;

namespace API.Controllers;

[ApiController]
[Route("api/bookings")]
public class BookingController : ControllerBase
{
    private readonly BookingService _bookingService;

    public BookingController(BookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [HttpGet("my/{userId}")]
    public async Task<IActionResult> GetBookingsByUserId(Guid userId)
    {
        var bookings = await _bookingService.GetBookingsByUserId(userId);
        return Ok(bookings);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBookingById(Guid id)
    {
        var booking = await _bookingService.GetBookingById(id);
        return Ok(booking);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBooking([FromBody] CreateBookingDto dto)
    {
        var booking = await _bookingService.CreateBooking(dto);
        return CreatedAtAction(nameof(GetBookingById), new { id = booking.Id }, booking);
    }

    [HttpPut("{bookingId}/cancel")]
    public async Task<IActionResult> CancelBooking(Guid bookingId, [FromBody] Guid userId)
    {
        await _bookingService.CancelBooking(bookingId, userId);
        return Ok();
    }
}