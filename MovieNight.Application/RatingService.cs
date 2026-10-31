using Microsoft.EntityFrameworkCore;
using MovieNight.Domain.Exceptions;
using MovieNight.Domain.Models;
using MovieNight.Infrastructure;

namespace MovieNight.Application;

public class RatingService(MovieNightDbContext context) : IRatingService
{
    private readonly MovieNightDbContext _context = context;
    public Task<MovieRating> CreateAsync(MovieRating movieRating, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteByMovieIdAndUsernameAsync(long movieId, string username, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ExistsByMovieIdAndUsernameAsync(long movieId, string username, CancellationToken cancellationToken = default)
    {
        return await _context
            .Ratings
            .AnyAsync(
                mr => mr.MovieId == movieId &&
                mr.UserUsername == username,
                cancellationToken
            );
    }

    public async Task<List<MovieRating>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context
            .Ratings
            .ToListAsync(cancellationToken);
    }

    public async Task<MovieRating> GetByMovieIdAndUsernameAsync(long movieId, string username, CancellationToken cancellationToken = default)
    {
        return await _context
            .Ratings
            .FirstOrDefaultAsync(
                mr => mr.MovieId == movieId &&
                mr.UserUsername == username,
                cancellationToken
            ) ??
            throw new EntityDoesNotExistException(
                "Rating", "MovieId and Username", $"({movieId}, {username})"
            );
    }

    public Task UpdateAsync(MovieRating newMovieRating, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}