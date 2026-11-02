using Microsoft.EntityFrameworkCore;
using MovieNight.Domain.Exceptions;
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

    public async Task<bool> ExistsByIdAsync(long movieId, CancellationToken cancellationToken = default)
    {
        return await _context
            .Movies
            .AnyAsync(m => m.MovieId == movieId, cancellationToken);
    }

    public Task<List<Movie>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<Movie> GetByIdAsync(long movieId, CancellationToken cancellationToken = default)
    {
        return await _context
            .Movies
            .Include(m => m.Actors)
            .Include(m => m.Ratings)
            .FirstOrDefaultAsync(
                m => m.MovieId == movieId, cancellationToken
            ) ??
            throw new EntityDoesNotExistException("Movie", "MovieId", movieId);
    }

    public Task UpdateAsync(Movie movie, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}