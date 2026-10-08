namespace MovieNight.Domain.Models;

public class User
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;

    public User() { }

    public User(string username, string password, string name, string email)
    {
        Username = username;
        Password = password;
        Name = name;
        Email = email;
    }

    public override bool Equals(object? obj)
    {
        if ((obj == null) || !GetType().Equals(obj.GetType()))
        {
            return false;
        }
        else
        {
            User maybeUser = (User)obj;
            return Username == maybeUser.Username;
        }
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Username);
    }
}