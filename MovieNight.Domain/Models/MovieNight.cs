namespace MovieNight.Domain.Models;

public class MovieNight
{
    public long MovieNightId { get; set; }
    public Movie MovieShown { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public User Host { get; set; } = null!;
    public HashSet<User> Participants { get; set; } = null!;
    public DateTime HeldOn { get; set; }

    public MovieNight(long movieNightId, Movie movieShown, string title, string? description, User host, HashSet<User>? participants, DateTime heldOn)
    {
        MovieNightId = movieNightId;
        MovieShown = movieShown;
        Title = title;
        Description = description ?? "";
        Host = host;
        Participants = participants ?? [];
        HeldOn = heldOn;
    }

    public MovieNight() { }
}