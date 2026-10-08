using MovieNight.Application;
using MovieNight.Domain.Exceptions;
using MovieNight.Domain.Models;
using MovieNight.Infrastructure;
using NUnit.Framework;

namespace MovieNight.Tests.Integration;

public class UserTests
{
    private MovieNightDbContext _context = null!;
    private UserService _service = null!;

    [SetUp]
    public async Task Setup()
    {
        var factory = new MovieNightDbContextFactory();
        _context = factory.CreateContext();
        var user = new User("harri40", "abcde", "Harry Jones", "hj@harrimail.com");
        _context.Users.Add(user);
        _context.SaveChanges();
        _service = new(_context);
    }

    [TearDown]
    public void Teardown()
    {
        _context.Dispose();
    }

    [Test]
    public async Task ExistsByUsernameReturnsTrueOnExistingUsername()
    {
        Assert.True(await _service.ExistsByUsernameAsync("harri40"));
    }

    [Test]
    public async Task ExistsByUsernameReturnsFalseOnNonExistentUsername()
    {
        Assert.False(await _service.ExistsByUsernameAsync("harri41"));
    }

    [Test]
    public async Task GetByUsernameReturnsExpectedUserOnExistingUsername()
    {
        var userFromDb = await _service.GetByUsernameAsync("harri40");
        Assert.AreEqual(userFromDb, new User("harri40", "abcde", "Harry Jones", "hj@harrimail.com"));
    }

    [Test]
    public async Task GetByUsernameThrowsOnNonExistentUsername()
    {
        var exception = await Assert.ThrowsAsync<EntityDoesNotExistException>(
            async () => await _service.GetByUsernameAsync("harri41")
        );

        Assert.AreEqual("No User with username \"harri41\" exists.", exception.Message);
    }

    [Test]
    public async Task GetAllReturnsAllUsers()
    {
        List<User> expectedList = [new User("harri40", "abcde", "Harry Jones", "hj@harrimail.com")];
        Assert.AreEqual(expectedList, await _service.GetAllAsync());
    }

    [Test]
    public async Task GetAllReturnsEmptyListIfNoUsers()
    {
        List<User> expectedList = [];
        _context.Users.Remove(_context.Users.First(u => u.Username == "harri40"));
        _context.SaveChanges();
        Assert.AreEqual(expectedList, await _service.GetAllAsync());
    }
}
