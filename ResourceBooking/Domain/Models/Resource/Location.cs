namespace Domain.Models;

public class Location
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public Location(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }
}