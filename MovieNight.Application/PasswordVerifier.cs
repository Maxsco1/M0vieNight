namespace MovieNight.Application;

public class PasswordVerifier
{
    public static bool VerifyPassword(string enteredPassword, string storedHash)
    {
        return BCrypt.Net.BCrypt.Verify(enteredPassword, storedHash);
    }
}
