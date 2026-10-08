namespace MovieNight.Domain.Models;

public class Movie
{
    public long MovieId { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public HashSet<MovieRating> Ratings { get; set; } = null!;
    public string Category { get; set; } = null!;
    public HashSet<Actor> Actors { get; set; } = null!;
    public DateOnly ReleaseDate { get; set; }
    public string PathToCoverImage { get; set; } = null!;

    public Movie(long movieId, string title, string description, HashSet<MovieRating>? ratings, string category, HashSet<Actor> actors, DateOnly releaseDate, string pathToCoverImage)
    {
        MovieId = movieId;
        Title = title;
        Description = description;
        Ratings = ratings ?? [];
        Category = category;
        Actors = actors;
        ReleaseDate = releaseDate;
        PathToCoverImage = pathToCoverImage;
    }

    public Movie() { }
}