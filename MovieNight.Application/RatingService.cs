using Microsoft.EntityFrameworkCore;
using MovieNight.Domain.Exceptions;
using MovieNight.Domain.Models;
using MovieNight.Infrastructure;

namespace MovieNight.Application;

public class RatingService(MovieNightDbContext context) : IRatingService
{
    private readonly MovieNightDbContext _context = context;
    public async Task<MovieRating> CreateAsync(MovieRating movieRating, CancellationToken cancellationToken = default)
    {
        if (await ExistsByMovieIdAndUsernameAsync(movieRating.MovieId, movieRating.UserUsername, cancellationToken))
        {
            throw new EntityAlreadyExistsException("MovieRating", "MovieId and Username", $"({movieRating.MovieId}, {movieRating.UserUsername})");
        }

        await VerifyRatingHasValidMovieIdAndUsername(movieRating.MovieId, movieRating.UserUsername, cancellationToken);

        await _context.Ratings.AddAsync(movieRating, cancellationToken);

        if (await _context.SaveChangesAsync(cancellationToken) < 1)
        {
            throw new ModifyFailedException("create", "MovieRating");
        }

        return movieRating;
    }

    public async Task DeleteByMovieIdAndUsernameAsync(long movieId, string username, CancellationToken cancellationToken = default)
    {
        await VerifyRatingHasValidMovieIdAndUsername(movieId, username, cancellationToken);
        var movieRating = await GetByMovieIdAndUsernameAsync(movieId, username, cancellationToken);

        _context.Ratings.Remove(movieRating);

        if (await _context.SaveChangesAsync(cancellationToken) < 1)
        {
            throw new ModifyFailedException("delete", "MovieRating");
        }
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

    private async Task VerifyRatingHasValidMovieIdAndUsername(long movieId, string username, CancellationToken cancellationToken)
    {
        if (!await _context.Movies.AnyAsync(m => m.MovieId == movieId, cancellationToken))
        {
            throw new EntityDoesNotExistException("Movie", "MovieId", movieId);
        }

        if (!await _context.Users.AnyAsync(u => u.Username == username, cancellationToken))
        {
            throw new EntityDoesNotExistException("User", "Username", username);
        }
    }
}