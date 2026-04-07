namespace Service.DTO.Location;

public class LocationDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public LocationDto(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}