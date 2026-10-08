using System.Data.Common;
using MovieNight.Application;
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
}
