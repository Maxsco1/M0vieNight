namespace MovieNight.Domain.Models.DTOs;

public class LoginInfoDTO(string username, string password)
{
    public string Username { get; set; } = username;
    public string Password { get; set; } = password;
}
