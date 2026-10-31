using Microsoft.EntityFrameworkCore;
using MovieNight.Application;
using MovieNight.Domain.Exceptions;
using MovieNight.Domain.Models;
using MovieNight.Infrastructure;
using NUnit.Framework;

namespace MovieNight.Tests.Integration;

public class MovieRatingTests
{
    private MovieNightDbContext _context = null!;
    private RatingService _service = null!;

    [SetUp]
    public async Task Setup()
    {
        var factory = new MovieNightDbContextFactory();
        _context = factory.CreateContext();
        var movie = new Movie(0, "example1", "desc1", [], "Horror", [], DateOnly.FromDayNumber(729635), "example.png");
        var user = new User("fakeuser", "123", "Fake User", "fakeuser@fakemail.com");
        var rating = new MovieRating(user, movie, 2);
        _context.Movies.Add(movie);
        _context.Users.Add(user);
        _context.Ratings.Add(rating);
        _context.SaveChanges();
        _service = new RatingService(_context);
    }

    [TearDown]
    public void Teardown()
    {
        _context.Dispose();
    }

    [Test]
    public async Task ExistsByMovieIdAndUsernameAsyncReturnsTrueOnExistingRating()
    {
        Assert.True(await _service.ExistsByMovieIdAndUsernameAsync(1, "fakeuser"));
    }

    [Test]
    public async Task ExistsByMovieIdAndUsernameAsyncReturnsFalseOnNonExistentRating()
    {
        Assert.False(await _service.ExistsByMovieIdAndUsernameAsync(0, "fakeuser"));
    }

    [Test]
    public async Task GetByMovieIdAndUsernameAsyncReturnsRatingIfExists()
    {
        Assert.True(await _service.ExistsByMovieIdAndUsernameAsync(1, "fakeuser"));
        var ratingFromDb = await _service.GetByMovieIdAndUsernameAsync(1, "fakeuser");
        Assert.AreEqual(1, ratingFromDb.MovieId);
        Assert.AreEqual("fakeuser", ratingFromDb.UserUsername);
        Assert.AreEqual(2, ratingFromDb.RatingStars);
    }

    [Test]
    public async Task GetByMovieIdAndUsernameAsyncThrowsOnNonExistentMovie()
    {
        var expectedMessage = "No Rating with MovieId and Username \"(0, someusername)\" exists.";

        var exception = await Assert.ThrowsAsync<EntityDoesNotExistException>(
            async () => await _service.GetByMovieIdAndUsernameAsync(0, "someusername")
        );

        Assert.AreEqual(expectedMessage, exception.Message);
    }

    [Test]
    public async Task GetByMovieIdAndUsernameAsyncThrowsOnNonExistentUser()
    {
        var expectedMessage = "No Rating with MovieId and Username \"(1, someusername)\" exists.";

        var exception = await Assert.ThrowsAsync<EntityDoesNotExistException>(
            async () => await _service.GetByMovieIdAndUsernameAsync(1, "someusername")
        );

        Assert.AreEqual(expectedMessage, exception.Message);
    }

    [Test]
    public async Task GetAllReturnsAListOfAllRatings()
    {
        var ratings = await _service.GetAllAsync();
        Assert.AreEqual(1, ratings.Count);

        var movieFromDb = await _service.GetByMovieIdAndUsernameAsync(1, "fakeuser");
        Assert.AreEqual(movieFromDb, ratings.First(mr => mr.MovieId == 1 && mr.UserUsername == "fakeuser"));
    }
}