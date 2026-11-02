using MovieNight.Domain.Models;

namespace MovieNight.Application;

public interface IMovieService
{
    Task<bool> ExistsByIdAsync(long movieId, CancellationToken cancellationToken = default);
    Task<Movie> GetByIdAsync(long movieId, CancellationToken cancellationToken = default);
    Task<List<Movie>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<MovieRating> CreateAsync(Movie movie, CancellationToken cancellationToken = default);
    Task UpdateAsync(Movie movie, CancellationToken cancellationToken = default);
    Task DeleteByIdAsync(long movieId, CancellationToken cancellationToken = default);
}