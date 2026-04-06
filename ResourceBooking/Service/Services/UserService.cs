using Domain.Models;
using Infrastructure.DTO;
using Service.Interfaces.Repositories;
using Service.Interfaces.Services;

namespace Service.Services;

public class UserService
{
    private readonly IUserRepository _userRepository;
    private readonly IHashService _hashService;

    public UserService(IUserRepository userRepository, IHashService hashService)
    {
        _userRepository = userRepository;
        _hashService = hashService;
    }

    public async Task<UserDto> CreateUser(CreateUserDto userDto)
    {
        if (await _userRepository.ExistsByEmail(userDto.Email))
        {
            throw new Exception("User already exists");
        }
        
        string hashPassword = ConvertPassword(userDto.Password);
        
        if (!Enum.TryParse<UserRole>(userDto.Role, true, out var role))
        {
            throw new ArgumentException("Invalid role");
        }
        
        User user = new User(userDto.Name, userDto.Email, hashPassword, role);
        await _userRepository.CreateUser(user);
        
        return ConvertUserToUserDto(user);
    }

    public async Task<UserDto> Login(EnterUserDto enterUserDto)
    {
        User? user = await _userRepository.GetUserByEmail(enterUserDto.Email);
        if (user == null)
            throw new Exception("Invalid email");
        
        string hashPassword = ConvertPassword(enterUserDto.Password);
        if (user.Password != hashPassword)
            throw new Exception("The password is entered incorrectly");
        
        UserDto dto = ConvertUserToUserDto(user);
        return dto;
    }

    public async Task<UserDto> GetUserByID(Guid userId)
    {
        User user = await _userRepository.GetUserById(userId);
        if (user == null)
            throw new Exception("Invalid ID");
        
        UserDto dto = ConvertUserToUserDto(user);
        return dto;
    }
    
    private string ConvertPassword(string password)
    {
        string hashPassword = _hashService.Hash(password);
        return hashPassword;
    }
    
    private UserDto ConvertUserToUserDto(User user)
    {
        UserDto dto = new UserDto(user.Id, user.Name, user.Email, user.Password, user.Role.ToString(), user.CreatedAt);
        return dto;
    }
}