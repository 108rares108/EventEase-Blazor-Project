namespace EventEase.Models;

public sealed class RegistrationRecord
{
    public int Id { get; set; }
    public int EventId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int Tickets { get; set; }
    public DateTime RegisteredAt { get; set; }
    public bool Attended { get; set; }

    public RegistrationRecord Copy() => new()
    {
        Id = Id,
        EventId = EventId,
        FullName = FullName,
        Email = Email,
        Tickets = Tickets,
        RegisteredAt = RegisteredAt,
        Attended = Attended
    };
}

public sealed record RegistrationResult(bool Success, string Message);
