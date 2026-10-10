namespace MovieNight.Domain.Models;

public class Actor
{
    public long ActorId { get; set; }
    public string Name { get; set; } = null!;
    public HashSet<Movie> Movies { get; set; } = [];

    public Actor() { }

    public Actor(long actorId, string name, HashSet<Movie> movies)
    {
        ActorId = actorId;
        Name = name;
        Movies = movies;
    }
}
