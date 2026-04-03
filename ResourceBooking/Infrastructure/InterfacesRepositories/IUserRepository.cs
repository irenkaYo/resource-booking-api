using Domain.Models;

namespace Infrastructure.InterfacesRepositories;

public interface IUserRepository
{
    public Task CreateUser(User user);
    public Task<User>? GetUserById(Guid userId);
}