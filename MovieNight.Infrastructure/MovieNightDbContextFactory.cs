using System.Data.Common;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace MovieNight.Infrastructure;

public class MovieNightDbContextFactory : IDisposable
{
    private DbConnection _connection = null!;

    private DbContextOptions<MovieNightDbContext> CreateOptions()
    {
        return new DbContextOptionsBuilder<MovieNightDbContext>()
            .UseSqlite(_connection)
            .Options;
    }

    public MovieNightDbContext CreateContext()
    {
        if (_connection == null)
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            var options = CreateOptions();
            using var context = new MovieNightDbContext(options);
            context.Database.EnsureCreated();
        }

        return new MovieNightDbContext(CreateOptions());
    }

    public void Dispose()
    {
        if (_connection != null)
        {
            _connection.Dispose();
            _connection = null!;
        }
    }
}
