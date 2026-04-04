namespace Domain.Models;

public class Booking
{
    public Guid Id { get; set; }
    public Guid ResourceId { get; set; }
    public Guid UserId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public BookingStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public Resource Resource { get; set; }

    public Booking(Guid resourceId, Guid userId, DateTime startTime, DateTime endTime)
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