namespace AngularAPI.Controllers;


using Microsoft.AspNetCore.Mvc;
using AngularAPI.Helpers;
using AngularAPI.Models;
using AngularAPI.Services;
using AngularAPI.Entities;


[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private IUsersService _userService;

    public UsersController(IUsersService userService)
    {
        _userService = userService;
    }

    [HttpPost("authenticate")]
    public IActionResult Authenticate(AuthenticateRequest model)
    {
        var response = _userService.Authenticate(model);

        if (response == null)
            return BadRequest(new { message = "Username or password is incorrect" });

        return Ok(response);
    }

    [HttpPost("register")]
    public IActionResult Register(RegistrationRequest model)
    {
        var response = _userService.Register(model);

        if (response == null)
            return BadRequest(new { message = "Error during registration" });

        return Ok(response);
    }


    [Authorize]
    [HttpPut("update")]
    public IActionResult Update(User user)
    {
        var response = _userService.UpdateUser(user);

        if (response == null)
            return BadRequest(new { message = "Error during update operation" });

        return Ok($"User {user.UserName} successfully updated");

    }

    [Authorize]
    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var user = _userService.GetById(id);
        return Ok(user);
    }

    [Authorize]
    [HttpGet]
    public IActionResult GetAll()
    {
        var users = _userService.GetAll();
        return Ok(users);
    }
}