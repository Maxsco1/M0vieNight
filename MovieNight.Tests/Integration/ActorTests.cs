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

    [Test]
    public async Task GetByIdReturnsExpectedActorIfActorWithGivenId()
    {
        var expectedActor = new Actor(1, "New Actor", []);
        Assert.AreEqual(expectedActor, await _service.GetByIdAsync(1));
    }

    [Test]
    public async Task GetByIdThrowsIfNoActorWithGivenId()
    {
        var expectedMessage = "No Actor with ActorId \"0\" exists.";

        var exception = await Assert.ThrowsAsync<EntityDoesNotExistException>(
            async () => await _service.GetByIdAsync(0)
        );

        Assert.AreEqual(expectedMessage, exception.Message);
    }

    [Test]
    public async Task GetAllAsyncReturnsAllActors()
    {
        List<Actor> expectedActors = [new Actor(1, "New Actor", [])];
        Assert.AreEqual(expectedActors, await _service.GetAllAsync());
    }

    [Test]
    public async Task CreateAsyncCreatesNewActorIfNoActorWithGivenIdExists()
    {
        var newActor = new Actor(0, "New Actor 2", []);
        await _service.CreateAsync(newActor);
        Assert.True(await _service.ExistsByIdAsync(newActor.ActorId));
    }

    [Test]
    public async Task CreateAsyncThrowsIfActorWithGivenIdExists()
    {
        var existingActor = new Actor(1, "New Actor", []);
        var expectedMessage = "A(n) Actor with ActorId \"1\" already exists.";

        var exception = await Assert.ThrowsAsync<EntityAlreadyExistsException>(
            async () => await _service.CreateAsync(existingActor)
        );

        Assert.AreEqual(expectedMessage, exception.Message);
    }

    [Test]
    public async Task DeleteByIdAsyncDeletesOnExistingActor()
    {
        Assert.True(await _service.ExistsByIdAsync(1));
        await _service.DeleteByIdAsync(1);
        Assert.False(await _service.ExistsByIdAsync(1));
    }

    [Test]
    public async Task DeleteByIdAsyncThrowsOnNonExistentActor()
    {
        var expectedMessage = "No Actor with ActorId \"0\" exists.";

        var exception = await Assert.ThrowsAsync<EntityDoesNotExistException>(
            async () => await _service.DeleteByIdAsync(0)
        );

        Assert.AreEqual(expectedMessage, exception.Message);
    }
}