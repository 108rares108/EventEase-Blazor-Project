using System.ComponentModel.DataAnnotations;

namespace EventEase.Models;

public sealed class EventItem
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Please enter an event title.")]
    [StringLength(80, MinimumLength = 3, ErrorMessage = "The title must be between 3 and 80 characters.")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please enter a location.")]
    [StringLength(120, ErrorMessage = "The location cannot exceed 120 characters.")]
    public string Location { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please add a short description.")]
    [StringLength(400, MinimumLength = 10, ErrorMessage = "The description must be between 10 and 400 characters.")]
    public string Description { get; set; } = string.Empty;

    public DateTime Date { get; set; }

    [Range(1, 1000, ErrorMessage = "Capacity must be between 1 and 1,000.")]
    public int Capacity { get; set; }

    public int AttendeeCount { get; set; }

    public int AvailableSeats => Math.Max(0, Capacity - AttendeeCount);

    public EventItem Copy() => new()
    {
        Id = Id,
        Title = Title,
        Location = Location,
        Description = Description,
        Date = Date,
        Capacity = Capacity,
        AttendeeCount = AttendeeCount
    };
}
