using MovieNight.Domain.Models;

namespace MovieNight.Application;

public interface IActorService
{
    Task<bool> ExistsByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<Actor> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<Actor> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<List<Actor>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Actor> CreateAsync(Actor Actor, CancellationToken cancellationToken = default);
    Task UpdateAsync(Actor newActor, CancellationToken cancellationToken = default);
    Task DeleteByIdAsync(long id, CancellationToken cancellationToken = default);
}