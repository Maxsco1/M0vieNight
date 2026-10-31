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

    public override bool Equals(object? obj)
    {
        if ((obj == null) || !GetType().Equals(obj.GetType()))
        {
            return false;
        }
        else
        {
            Actor maybeActor = (Actor)obj;
            return ActorId == maybeActor.ActorId;
        }
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(ActorId);
    }
}
