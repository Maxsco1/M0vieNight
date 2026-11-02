using Microsoft.EntityFrameworkCore;
using MovieNight.Domain.Exceptions;
using MovieNight.Domain.Models;
using MovieNight.Infrastructure;

namespace MovieNight.Application;

public class MovieService(MovieNightDbContext context) : IMovieService
{
    private readonly MovieNightDbContext _context = context;
    public async Task<Movie> CreateAsync(Movie movie, CancellationToken cancellationToken = default)
    {
        if (await ExistsByIdAsync(movie.MovieId, cancellationToken))
        {
            throw new EntityAlreadyExistsException("Movie", "MovieId", movie.MovieId);
        }

        await _context.Movies.AddAsync(movie, cancellationToken);
        await AddNewActorsToDb(movie.Actors);

        if (await _context.SaveChangesAsync(cancellationToken) < 1)
        {
            throw new ModifyFailedException("create", "MovieRating");
        }

        return movie;
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

    public async Task<List<Movie>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context
            .Movies
            .Include(m => m.Actors)
            .Include(m => m.Ratings)
            .ToListAsync(cancellationToken);
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

    private async Task AddNewActorsToDb(HashSet<Actor> actorsFromMovie)
    {
        foreach (var actor in actorsFromMovie)
        {
            if (!await _context.Actors.AnyAsync(a => a.ActorId == actor.ActorId))
            {
                await _context.Actors.AddAsync(actor);
            }
        }
    }
}