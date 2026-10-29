using MovieNight.Application;
using MovieNight.Domain.Exceptions;
using MovieNight.Domain.Models;
using MovieNight.Infrastructure;
using NUnit.Framework;

namespace MovieNight.Tests.Integration;

public class ActorTests
{
    private MovieNightDbContext _context = null!;
    private ActorService _service = null!;

    [SetUp]
    public async Task Setup()
    {
        var factory = new MovieNightDbContextFactory();
        _context = factory.CreateContext();
        var actor = new Actor(0, "New Actor", []);
        _context.Actors.Add(actor);
        _context.SaveChanges();
        _service = new(_context);
    }

    [TearDown]
    public void Teardown()
    {
        _context.Dispose();
    }

    [Test]
    public async Task ExistsByIdReturnsTrueIfActorWithGivenId()
    {
        Assert.True(await _service.ExistsByIdAsync(1)); // "New Actor" gets ID 1.
    }

    [Test]
    public async Task ExistsByIdReturnsFalseIfNoActorWithGivenId()
    {
        Assert.False(await _service.ExistsByIdAsync(0));
    }
}