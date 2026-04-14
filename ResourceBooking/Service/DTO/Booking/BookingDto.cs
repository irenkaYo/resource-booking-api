namespace Infrastructure.DTO;

public class BookingDto
{
    public Guid Id { get; set; }
    public Guid ResourceId { get; set; }
    public Guid UserId { get; set; }
    public DateTimeOffset StartTime { get; set; }
    public DateTimeOffset EndTime { get; set; }
    public string Status { get; set; }
    public DateTimeOffset CreatedAt { get; set; }

    public BookingDto(Guid id, Guid resourceId, Guid userId, DateTimeOffset startTime, DateTimeOffset endTime, string status, DateTimeOffset createdAt)
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