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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasKey(u => u.Username);

        modelBuilder.Entity<MovieRating>()
            .HasKey(mr => new { mr.MovieId, mr.UserUsername });

        modelBuilder.Entity<MovieRating>()
            .HasOne(mr => mr.Movie)
            .WithMany()
            .HasForeignKey(mr => mr.MovieId);

        modelBuilder.Entity<MovieRating>()
            .HasOne(mr => mr.User)
            .WithMany()
            .HasForeignKey(mr => mr.UserUsername);
    }
}