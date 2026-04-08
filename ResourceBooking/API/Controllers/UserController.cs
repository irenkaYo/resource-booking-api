using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Infrastructure.DTO;
using Microsoft.AspNetCore.Mvc;
using Service.Services;

namespace API.Controllers;

[ApiController]
[Route("api/auth")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;
    
    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto userDto)
    {
        var user = await _userService.CreateUser(userDto);
        return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        var user = await _userService.GetUserByID(id);
        return Ok(user);
    }
}