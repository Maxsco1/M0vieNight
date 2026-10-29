using Microsoft.EntityFrameworkCore;
using MovieNight.Domain.Exceptions;
using MovieNight.Domain.Models;
using MovieNight.Infrastructure;

namespace MovieNight.Application;

public class ActorService(MovieNightDbContext context) : IActorService
{
    private readonly MovieNightDbContext _context = context;
    public Task<Actor> CreateAsync(Actor Actor, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ExistsByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        return await _context
            .Actors
            .AnyAsync(a => a.ActorId == id, cancellationToken);
    }

    public Task<List<Actor>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<Actor> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        return await _context
            .Actors
            .FirstOrDefaultAsync(a => a.ActorId == id, cancellationToken) ??
            throw new EntityDoesNotExistException("Actor", "ActorId", id);
    }

    public Task<Actor> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Actor newActor, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}