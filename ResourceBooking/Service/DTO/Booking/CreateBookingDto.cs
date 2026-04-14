namespace Infrastructure.DTO;

public class CreateBookingDto
{
    public Guid ResourceId { get; set; }
    public Guid UserId { get; set; }
    public DateTimeOffset StartTime { get; set; }
    public DateTimeOffset EndTime { get; set; }
}