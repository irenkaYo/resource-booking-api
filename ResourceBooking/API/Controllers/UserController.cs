using Infrastructure.DTO;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces.Services;

namespace API.Controllers;

[ApiController]
[Route("api/auth")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto userDto)
    {
        var user = await _userService.CreateUser(userDto);
        return CreatedAtAction(nameof(GetUserById), new { userId = user.Id }, user);
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserById(Guid userId)
    {
        var user = await _userService.GetUserByID(userId);
        return Ok(user);
    }
}