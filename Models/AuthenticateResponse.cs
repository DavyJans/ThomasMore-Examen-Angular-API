using AngularAPI.Entities;

namespace AngularAPI.Models;

public class AuthenticateResponse
{
    public string Token { get; set; }

    public User User { get; set; }


    public AuthenticateResponse(User user, string token)
    {
        User = user;
        Token = token;

    }
}