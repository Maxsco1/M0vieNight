namespace MovieNight.Domain.Models;

public class MovieRating
{
    public User ByUser { get; set; } = null!;
    public Movie OnMovie { get; set; } = null!;
    public int RatingStars { get; set; }

    public MovieRating() { }

    public MovieRating(User byUser, Movie onMovie, int stars)
    {
        ByUser = byUser;
        OnMovie = onMovie;
        RatingStars = stars;
    }
}