namespace Infrastructure.DTO;

public class UserDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public UserDto(Guid id, string name, string email, string role, DateTime createdAt)
    {
        Id = id;
        Name = name;
        Email = email;
        Role = role;
        CreatedAt = createdAt;
    }
}