namespace Domain.Models;

public class Booking
{
    public Guid Id { get; set; }
    public Guid ResourceId { get; set; }
    public Guid UserId { get; set; }
    public DateTimeOffset StartTime { get; set; }
    public DateTimeOffset EndTime { get; set; }
    public BookingStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public Resource Resource { get; set; }

    public Booking(Guid resourceId, Guid userId, DateTimeOffset startTime, DateTimeOffset endTime)
    {
        Id = Guid.NewGuid();
        ResourceId = resourceId;
        UserId = userId;
        StartTime = startTime;
        EndTime = endTime;
        Status = BookingStatus.Active;
        CreatedAt = DateTime.UtcNow;
    }
}