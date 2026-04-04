using Domain.Models;

namespace Infrastructure.DTO;

public class BookingDto
{
    public Guid Id { get; set; }
    public Guid ResourceId { get; set; }
    public Guid UserId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public BookingStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }

    public BookingDto(Guid id, Guid resourceId, Guid userId, DateTime startTime, DateTime endTime, BookingStatus status, DateTime createdAt)
    {
        Id = id;
        ResourceId = resourceId;
        UserId = userId;
        StartTime = startTime;
        EndTime = endTime;
        Status = status;
        CreatedAt = createdAt;
    }
}