using Microsoft.EntityFrameworkCore;
using MovieNight.Domain.Models;
using MovieNight.Infrastructure;

namespace MovieNight.Application;

public class RatingService(MovieNightDbContext context) : IRatingService
{
    private readonly MovieNightDbContext _context = context;
    public Task<MovieRating> CreateAsync(MovieRating movieRating, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteByMovieIdAndUsernameAsync(long movieId, string username, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsByMovieIdAndUsernameAsync(long movieId, string username, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<MovieRating>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<MovieRating> GetByMovieIdAndUsernameAsync(long movieId, string username, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(MovieRating newMovieRating, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}