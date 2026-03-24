namespace Domain.Models;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }//как в бд сохранять хэшированный пароль
    public UserRole Role { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<Booking> Bookings { get; set; }
}