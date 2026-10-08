using Microsoft.EntityFrameworkCore;
using MovieNight.Domain.Exceptions;
using MovieNight.Domain.Models;
using MovieNight.Infrastructure;

namespace MovieNight.Application;

public class UserService(MovieNightDbContext context) : IUserService
{
    private readonly MovieNightDbContext _context = context;

    public async Task<User> CreateAsync(User user, CancellationToken cancellationToken = default)
    {
        if (await ExistsByUsernameAsync(user.Username, cancellationToken))
        {
            throw new EntityAlreadyExistsException("User", "username", user.Username);
        }

        await _context.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return user;
    }

    public Task DeleteByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ExistsByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        return await _context.Users
            .AnyAsync(u => u.Username == username, cancellationToken);
    }

    public async Task<User> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Username == username, cancellationToken) ??
            throw new EntityDoesNotExistException("User", "username", username);
    }

    public async Task<List<User>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Users.ToListAsync(cancellationToken);
    }

    public Task<User> UpdateAsync(User newUser, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}