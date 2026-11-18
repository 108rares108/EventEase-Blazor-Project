using EventEase.Models;

namespace EventEase.Services;

/// <summary>
/// Demo data store for the current Blazor Server circuit.
/// Replace this service with a database-backed repository for production use.
/// </summary>
public sealed class EventService
{
    private readonly List<EventItem> _events =
    [
        new EventItem
        {
            Id = 1,
            Title = "Digital Innovation Summit",
            Location = "Bucharest, Romania",
            Description = "A day of practical talks and conversations about technology, product ideas, and digital business.",
            Date = new DateTime(2026, 12, 12, 9, 30, 0),
            Capacity = 120,
            AttendeeCount = 68
        },
        new EventItem
        {
            Id = 2,
            Title = "Creative Design Meetup",
            Location = "Cluj-Napoca, Romania",
            Description = "Meet local designers, share work in progress, and learn new ways to approach creative problems.",
            Date = new DateTime(2027, 1, 15, 18, 0, 0),
            Capacity = 60,
            AttendeeCount = 24
        },
        new EventItem
        {
            Id = 3,
            Title = "Startup Networking Night",
            Location = "Iași, Romania",
            Description = "An informal evening for founders, students, and people interested in building new projects.",
            Date = new DateTime(2027, 2, 4, 17, 30, 0),
            Capacity = 80,
            AttendeeCount = 51
        }
    ];

    private readonly List<RegistrationRecord> _registrations = [];
    private int _nextRegistrationId = 1;

    public List<EventItem> GetEvents() => _events
        .OrderBy(item => item.Date)
        .Select(item => item.Copy())
        .ToList();

    public EventItem? GetEventById(int id) => _events
        .Where(item => item.Id == id)
        .Select(item => item.Copy())
        .FirstOrDefault();

    public bool UpdateEvent(EventItem updatedEvent)
    {
        var existing = _events.FirstOrDefault(item => item.Id == updatedEvent.Id);
        if (existing is null)
        {
            return false;
        }

        existing.Title = updatedEvent.Title.Trim();
        existing.Location = updatedEvent.Location.Trim();
        existing.Description = updatedEvent.Description.Trim();
        existing.Capacity = Math.Max(updatedEvent.Capacity, existing.AttendeeCount);
        return true;
    }

    public RegistrationResult TryRegister(int eventId, RegistrationInput input)
    {
        var selectedEvent = _events.FirstOrDefault(item => item.Id == eventId);
        if (selectedEvent is null)
        {
            return new RegistrationResult(false, "This event could not be found.");
        }

        var email = input.Email.Trim();
        var alreadyRegistered = _registrations.Any(registration =>
            registration.EventId == eventId &&
            string.Equals(registration.Email, email, StringComparison.OrdinalIgnoreCase));

        if (alreadyRegistered)
        {
            return new RegistrationResult(false, "This email address is already registered for the selected event.");
        }

        if (input.Tickets < 1 || input.Tickets > 5)
        {
            return new RegistrationResult(false, "Choose between 1 and 5 tickets.");
        }

        if (selectedEvent.AvailableSeats < input.Tickets)
        {
            return new RegistrationResult(false, "There are not enough seats available for that number of tickets.");
        }

        _registrations.Add(new RegistrationRecord
        {
            Id = _nextRegistrationId++,
            EventId = eventId,
            FullName = input.FullName.Trim(),
            Email = email,
            Tickets = input.Tickets,
            RegisteredAt = DateTime.Now,
            Attended = false
        });

        selectedEvent.AttendeeCount += input.Tickets;
        return new RegistrationResult(true, "Registration completed successfully.");
    }

    public List<RegistrationRecord> GetRegistrations() => _registrations
        .OrderByDescending(item => item.RegisteredAt)
        .Select(item => item.Copy())
        .ToList();

    public bool ToggleAttendance(int registrationId)
    {
        var registration = _registrations.FirstOrDefault(item => item.Id == registrationId);
        if (registration is null)
        {
            return false;
        }

        registration.Attended = !registration.Attended;
        return true;
    }
}
