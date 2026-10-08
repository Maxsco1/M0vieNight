using MovieNight.Domain.Models;

namespace MovieNight.Application;

public interface IUserService
{
    Task<bool> ExistsByUsernameAsync(string username, CancellationToken cancellationToken = default);
    Task<User> GetByUsernameAsync(string username, CancellationToken cancellationToken = default);
    Task<List<User>> GetUsersAsync(CancellationToken cancellationToken = default);
    Task<User> CreateAsync(User user, CancellationToken cancellationToken = default);
    Task<User> UpdateAsync(User newUser, CancellationToken cancellationToken = default);
    Task DeleteByUsernameAsync(string username, CancellationToken cancellationToken = default);
}