namespace Infrastructure.DTO;

public class UserDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public UserDto(Guid id, string name, string email, string password, string role, DateTime createdAt)
    {
        Id = id;
        Name = name;
        Email = email;
        Password = password;
        Role = role;
        CreatedAt = createdAt;
    }
}