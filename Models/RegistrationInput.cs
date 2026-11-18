using System.ComponentModel.DataAnnotations;

namespace EventEase.Models;

public sealed class RegistrationInput
{
    [Required(ErrorMessage = "Please enter your name.")]
    [StringLength(80, MinimumLength = 2, ErrorMessage = "Your name must be between 2 and 80 characters.")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please enter your email address.")]
    [EmailAddress(ErrorMessage = "Enter a valid email address.")]
    public string Email { get; set; } = string.Empty;

    [Range(1, 5, ErrorMessage = "Choose between 1 and 5 tickets.")]
    public int Tickets { get; set; } = 1;
}
