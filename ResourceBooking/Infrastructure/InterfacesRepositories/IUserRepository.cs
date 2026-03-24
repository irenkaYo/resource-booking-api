using Domain.Models;

namespace Infrastructure.InterfacesRepositories;

public interface IUserRepository
{
    public Task<User> CreateUser(User user);
    public Task EnterToAccount(Guid userId);
}