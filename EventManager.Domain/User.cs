using Microsoft.AspNetCore.Identity;

namespace EventManager.Domain;

public class User : IdentityUser<Guid>
{
    public override Guid Id { get; set; }

    public List<Event> Events { get; set; } = new();
}