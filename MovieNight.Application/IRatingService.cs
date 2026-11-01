using MovieNight.Domain.Models;

namespace MovieNight.Application;

public interface IRatingService
{
    Task<bool> ExistsByMovieIdAndUsernameAsync(long movieId, string username, CancellationToken cancellationToken = default);
    Task<MovieRating> GetByMovieIdAndUsernameAsync(long movieId, string username, CancellationToken cancellationToken = default);
    Task<List<MovieRating>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<MovieRating> CreateAsync(MovieRating movieRating, CancellationToken cancellationToken = default);
    Task UpdateByMovieIdAndUsernameAsync(MovieRating newMovieRating, CancellationToken cancellationToken = default);
    Task DeleteByMovieIdAndUsernameAsync(long movieId, string username, CancellationToken cancellationToken = default);
}