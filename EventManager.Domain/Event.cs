namespace EventManager.Domain;

public class Event
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string Plan { get; set; }

    public Speaker Speaker { get; set; } = new();

    public User Organizer { get; set; } = new();

    public Location Location { get; set; } = new();
}