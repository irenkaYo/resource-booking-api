using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Infrastructure.DTO;
using Microsoft.AspNetCore.Mvc;
using Service.Services;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace API.Controllers;

[ApiController]
[Route("auth")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;
    private readonly IValidator<CreateUserDto> _validator;
    
    public UserController(UserService userService, IValidator<CreateUserDto> validator)
    {
        _userService = userService;
        _validator = validator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto userDto)
    {
        ValidationResult result = await _validator.ValidateAsync(userDto);
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