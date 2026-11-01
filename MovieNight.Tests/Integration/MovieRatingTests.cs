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
        var movie2 = new Movie(0, "example2", "desc2", [], "Thriller", [], DateOnly.FromDayNumber(729635), "example2.png");
        var user = new User("fakeuser", "123", "Fake User", "fakeuser@fakemail.com");
        var rating = new MovieRating(user, movie, 2);
        _context.Movies.Add(movie);
        _context.Movies.Add(movie2);
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

    [Test]
    public async Task CreateAsyncCreatesRatingIfMovieAndUserExistAndRatingDoesNotAlreadyExist()
    {
        var newRating = new MovieRating { UserUsername = "fakeuser", MovieId = 2 };
        var allRatingsBefore = await _service.GetAllAsync();

        await _service.CreateAsync(newRating);
        var allRatingsAfter = await _service.GetAllAsync();

        Assert.AreEqual(1, allRatingsBefore.Count);
        Assert.AreEqual(2, allRatingsAfter.Count);
        Assert.AreEqual(allRatingsAfter.Last(), newRating);
    }

    [Test]
    public async Task CreateAsyncThrowsIfRatingAlreadyExists()
    {
        var newRating = new MovieRating { UserUsername = "fakeuser", MovieId = 1 };
        var expectedMessage = "A(n) MovieRating with MovieId and Username \"(1, fakeuser)\" already exists.";

        var exception = await Assert.ThrowsAsync<EntityAlreadyExistsException>(
            async () => await _service.CreateAsync(newRating)
        );

        Assert.AreEqual(expectedMessage, exception.Message);
    }

    [Test]
    public async Task CreateAsyncThrowsOnNonExistentMovie()
    {
        var newRating = new MovieRating { MovieId = 0, UserUsername = "fakeOoser" };
        var expectedMessage = "No Movie with MovieId \"0\" exists.";

        var exception = await Assert.ThrowsAsync<EntityDoesNotExistException>(
            async () => await _service.CreateAsync(newRating)
        );

        Assert.AreEqual(expectedMessage, exception.Message);
    }

    [Test]
    public async Task CreateAsyncThrowsOnNonExistentUser()
    {
        var expectedMessage = "No User with Username \"fakeooser\" exists.";
        var newRating = new MovieRating { MovieId = 1, UserUsername = "fakeooser" };

        var exception = await Assert.ThrowsAsync<EntityDoesNotExistException>(
            async () => await _service.CreateAsync(newRating)
        );

        Assert.AreEqual(expectedMessage, exception.Message);
    }
}