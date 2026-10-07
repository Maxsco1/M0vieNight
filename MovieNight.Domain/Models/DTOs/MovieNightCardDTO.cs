namespace MovieNight.Domain.Models.DTOs;

public class MovieNightCardDTO(string moviePosterPath, string movieNightTitle, string userNameHosting, DateTime dateHeld)
{
    public string MoviePosterPath { get; set; } = moviePosterPath;
    public string MovieNightTitle { get; set; } = movieNightTitle;
    public string UserNameHosting { get; set; } = userNameHosting;
    public DateTime DateHeld { get; set; } = dateHeld;

    public static MovieNightCardDTO FromMovieNight(MovieNight movieNight)
    {
        return new MovieNightCardDTO(
            movieNight.MovieShown.PathToCoverImage,
            movieNight.Title,
            movieNight.Host.Name,
            movieNight.HeldOn
        );
    }
}