using Infrastructure.DTO;

namespace Service.Interfaces.Services;

public interface IUserService
{
    public Task<UserDto> CreateUser(CreateUserDto userDto);
    public Task<UserDto> Login(EnterUserDto enterUserDto);
    public Task<UserDto> GetUserByID(Guid userId);
}