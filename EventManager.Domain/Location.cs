namespace EventManager.Domain;

public class Location
{
    public Guid Id { get; set; }

    public string CityName { get; set; }

    public List<Event> Events { get; set; } = new();
}