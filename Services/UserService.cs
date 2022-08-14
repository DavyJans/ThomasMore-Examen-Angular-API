namespace AngularAPI.Services;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AngularAPI.Entities;
using AngularAPI.Helpers;
using AngularAPI.Models;
using AngularAPI.Data;

public interface IUsersService
{
    AuthenticateResponse Authenticate(AuthenticateRequest model);
    AuthenticateResponse Register(RegistrationRequest model);
    IEnumerable<User> GetAll();
    User GetById(int id);

    User UpdateUser(User user);
}

public class UserService : IUsersService
{
    // users hardcoded for simplicity, store in a db with hashed passwords in production applications
    
    private readonly AppSettings _appSettings;
    private readonly DataContext dataContext;

    public UserService(IOptions<AppSettings> appSettings, DataContext dataContext)
    {
        _appSettings = appSettings.Value;
        this.dataContext = dataContext;
    }

    public AuthenticateResponse Authenticate(AuthenticateRequest model)
    {
        var user = dataContext.Users.SingleOrDefault(x => x.UserName == model.UserName && x.Password == model.Password);

        // return null if user not found
        if (user == null) return null;

        // authentication successful so generate jwt token
        var token = generateJwtToken(user);

        return new AuthenticateResponse(user, token);
    }

    public AuthenticateResponse Register(RegistrationRequest model)
    {
        var user = new User
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            UserName = model.UserName,
            Password = model.Password,
            Role = Role.User
        };

        dataContext.Users.Add(user);
        var result = dataContext.SaveChanges();

        if (result <= 0) return null;

        // return null if user not found
        if (user == null) return null;

        // authentication successful so generate jwt token
        var token = generateJwtToken(user);

        return new AuthenticateResponse(user, token);
    }

    public IEnumerable<User> GetAll()
    {
        return dataContext.Users;
    }

    public User GetById(int id)
    {
        return dataContext.Users.FirstOrDefault(x => x.Id == id);
    }

    public User UpdateUser(User user)
    {
        if (user == null) return null;

        dataContext.Users.Update(user);

        var result = dataContext.SaveChanges();

        if (result <= 0) return null;

        return user;

    }

    // helper methods

    private string generateJwtToken(User user)
    {
        // generate token that is valid for 7 days
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}