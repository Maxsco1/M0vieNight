namespace MovieNight.Domain.Models;

public class Actor(long actorId, string name, HashSet<Movie> movies)
{
    public long ActorId { get; set; } = actorId;
    public string Name { get; set; } = name;
    public HashSet<Movie> Movies { get; set; } = movies;
}
