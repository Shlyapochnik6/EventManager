namespace EventManager.Domain;

public class Organizer
{
    public Guid Id { get; set; }

    public string OrganizerName { get; set; }

    public List<Event> Events { get; set; } = new();
}