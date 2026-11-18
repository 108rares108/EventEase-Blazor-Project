namespace EventEase.Services;

/// <summary>
/// Minimal per-circuit session state for the demo registration flow.
/// This is not a replacement for secure ASP.NET Core authentication.
/// </summary>
public sealed class SessionState
{
    public string? CurrentUserName { get; private set; }

    public bool IsSignedIn => !string.IsNullOrWhiteSpace(CurrentUserName);

    public void SignIn(string fullName)
    {
        CurrentUserName = fullName.Trim();
    }

    public void SignOut()
    {
        CurrentUserName = null;
    }
}
