using MovieNight.Domain.Models;
using MovieNight.Infrastructure;

namespace MovieNight.Application;

public class MovieService(MovieNightDbContext context) : IMovieService
{
    private readonly MovieNightDbContext _context = context;
    public Task<MovieRating> CreateAsync(Movie movie, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteByIdAsync(long movieId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsByIdAsync(long movieId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<Movie>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Movie> GetByMovieId(long movieId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Movie movie, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}