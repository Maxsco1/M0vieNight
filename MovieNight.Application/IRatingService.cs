using MovieNight.Domain.Models;

namespace MovieNight.Application;

public interface IRatingService
{
    Task<bool> ExistsByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<MovieRating> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<List<MovieRating>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<MovieRating> CreateAsync(MovieRating movieRating, CancellationToken cancellationToken = default);
    Task UpdateAsync(MovieRating newMovieRating, CancellationToken cancellationToken = default);
    Task DeleteByIdAsync(long id, CancellationToken cancellationToken = default);
}