namespace Domain.Models;

public class Feature
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public Feature(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }
}