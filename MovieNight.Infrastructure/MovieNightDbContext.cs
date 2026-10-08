using Microsoft.EntityFrameworkCore;
using MovieNight.Domain.Models;

namespace MovieNight.Infrastructure;

public class MovieNightDbContext(DbContextOptions<MovieNightDbContext> options) : DbContext(options)
{
    public DbSet<Actor> Actors => Set<Actor>();
    public DbSet<Movie> Movies => Set<Movie>();
    public DbSet<Domain.Models.MovieNight> MovieNights => Set<Domain.Models.MovieNight>();
    public DbSet<MovieRating> Ratings => Set<MovieRating>();
    public DbSet<User> Users => Set<User>();
}