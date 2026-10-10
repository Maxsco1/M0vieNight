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

        user.Password = PasswordHasher.HashPassword(user.Password);
        await _context.AddAsync(user, cancellationToken);

        if (!(await _context.SaveChangesAsync(cancellationToken) > 0))
        {
            throw new ModifyFailedException("create", "User");
        }

        return user;
    }

    public async Task DeleteByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        User user = await GetByUsernameAsync(username, cancellationToken);
        _context.Users.Remove(user);

        if (!(await _context.SaveChangesAsync(cancellationToken) > 0))
        {
            throw new ModifyFailedException("delete", "User");
        }
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

    public async Task UpdateAsync(User newUser, CancellationToken cancellationToken = default)
    {
        if (!await ExistsByUsernameAsync(newUser.Username, cancellationToken))
        {
            throw new EntityDoesNotExistException("User", "username", newUser.Username);
        }

        var userFromDb = await GetByUsernameAsync(newUser.Username, cancellationToken);
        userFromDb.Email = newUser.Email;
        userFromDb.Password = PasswordHasher.HashPassword(newUser.Password);
        userFromDb.Name = newUser.Name;

        _context.Update(userFromDb);
        if (!(await _context.SaveChangesAsync(cancellationToken) > 0))
        {
            throw new ModifyFailedException("update", "User");
        }
    }
}