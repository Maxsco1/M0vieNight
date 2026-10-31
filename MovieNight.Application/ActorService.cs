using Microsoft.EntityFrameworkCore;
using MovieNight.Domain.Exceptions;
using MovieNight.Domain.Models;
using MovieNight.Infrastructure;

namespace MovieNight.Application;

public class ActorService(MovieNightDbContext context) : IActorService
{
    private readonly MovieNightDbContext _context = context;
    public async Task<Actor> CreateAsync(Actor actor, CancellationToken cancellationToken = default)
    {
        if (await ExistsByIdAsync(actor.ActorId, cancellationToken))
        {
            throw new EntityAlreadyExistsException("Actor", "ActorId", actor.ActorId);
        }

        await _context.AddAsync(actor, cancellationToken);

        if (await _context.SaveChangesAsync(cancellationToken) < 1)
        {
            throw new ModifyFailedException("create", "Actor");
        }

        return actor;
    }

    public async Task DeleteByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        var actor = await GetByIdAsync(id, cancellationToken);
        _context.Remove(actor);

        if (await _context.SaveChangesAsync(cancellationToken) < 1)
        {
            throw new ModifyFailedException("delete", "Actor");
        }
    }

    public async Task<bool> ExistsByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        return await _context
            .Actors
            .AnyAsync(a => a.ActorId == id, cancellationToken);
    }

    public async Task<List<Actor>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context
            .Actors
            .Include(a => a.Movies)
            .ToListAsync(cancellationToken);
    }

    public async Task<Actor> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        return await _context
            .Actors
            .Include(a => a.Movies)
            .FirstOrDefaultAsync(a => a.ActorId == id, cancellationToken) ??
            throw new EntityDoesNotExistException("Actor", "ActorId", id);
    }

    public async Task UpdateAsync(Actor newActor, CancellationToken cancellationToken = default)
    {
        var actor = await GetByIdAsync(newActor.ActorId, cancellationToken);
        actor.Name = newActor.Name;
        await HandleUpdateActorMovies(actor, actor.Movies, newActor.Movies);
        await _context.SaveChangesAsync(cancellationToken);

    }

    private static async Task HandleUpdateActorMovies(Actor actor, HashSet<Movie> previousMovies, HashSet<Movie> newMovies)
    {
        foreach (var movie in newMovies)
        {
            if (!previousMovies.Contains(movie))
            {
                actor.Movies.Add(movie);
            }
        }

        foreach (var movie in previousMovies)
        {
            if (!newMovies.Contains(movie))
            {
                actor.Movies.Remove(movie);
            }
        }
    }
}