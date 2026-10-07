namespace MovieNight.Application;

public class PasswordHasher
{
    public static string HashPassword(string password, int workFactor = 12)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, workFactor);
    }
}
