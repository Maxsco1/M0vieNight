using MovieNight.Application;
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
}