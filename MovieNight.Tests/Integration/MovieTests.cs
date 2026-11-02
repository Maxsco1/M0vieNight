using MovieNight.Application;
using MovieNight.Domain.Exceptions;
using MovieNight.Domain.Models;
using MovieNight.Infrastructure;
using NUnit.Framework;

namespace MovieNight.Tests.Integration;

public class MovieTests
{
    private MovieNightDbContext _context = null!;
    private MovieService _service = null!;

    [SetUp]
    public async Task Setup()
    {
        var factory = new MovieNightDbContextFactory();
        _context = factory.CreateContext();
        var movie = new Movie(0, "example1", "desc1", [], "Horror", [], DateOnly.FromDayNumber(729635), "example.png");
        var movie2 = new Movie(0, "example2", "desc2", [], "Thriller", [], DateOnly.FromDayNumber(729635), "example2.png");
        _context.Movies.Add(movie);
        _context.Movies.Add(movie2);
        _context.SaveChanges();
        _service = new MovieService(_context);
    }

    [TearDown]
    public void Teardown()
    {
        _context.Dispose();
    }

    [Test]
    public async Task ExistsByIdReturnsTrueOnExistingMovie()
    {
        Assert.True(await _service.ExistsByIdAsync(1));
    }

    [Test]
    public async Task ExistsByIdReturnsFalseOnNonExistentMovie()
    {
        Assert.False(await _service.ExistsByIdAsync(0));
    }

    [Test]
    public async Task GetByIdReturnsExistingMovie()
    {
        var expectedMovie = new Movie(1, "example1", "desc1", [], "Horror", [], DateOnly.FromDayNumber(729635), "example.png");
        Assert.AreEqual(expectedMovie, await _service.GetByIdAsync(1));
    }

    [Test]
    public async Task GetByIdThrowsOnNonExistentMovie()
    {
        var expectedMessage = "No Movie with MovieId \"0\" exists.";

        var exception = await Assert.ThrowsAsync<EntityDoesNotExistException>(
            async () => await _service.GetByIdAsync(0)
        );

        Assert.AreEqual(expectedMessage, exception.Message);
    }

    [Test]
    public async Task GetAllReturnsAllMovies()
    {
        List<Movie> expectedMovies = [
            new Movie(1, "example1", "desc1", [], "Horror", [], DateOnly.FromDayNumber(729635), "example.png"),
            new Movie(2, "example2", "desc2", [], "Thriller", [], DateOnly.FromDayNumber(729635), "example2.png")
        ];

        Assert.AreEqual(expectedMovies, await _service.GetAllAsync());
    }
}