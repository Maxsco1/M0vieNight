namespace MovieNight.Domain.Models;

public class MovieRating
{
    public User User { get; set; } = null!;
    public Movie Movie { get; set; } = null!;
    public int RatingStars { get; set; }

    public MovieRating() { }

    public MovieRating(User byUser, Movie onMovie, int stars)
    {
        User = byUser;
        Movie = onMovie;
        RatingStars = stars;
    }
}