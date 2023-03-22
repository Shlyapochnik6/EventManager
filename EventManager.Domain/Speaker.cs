namespace EventManager.Domain;

public class Speaker
{
    public Guid Id { get; set; }

    public string SpeakerName { get; set; }

    public List<Event> Events { get; set; } = new();
}