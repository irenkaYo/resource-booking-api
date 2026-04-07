using Domain.Models;

namespace Service.Interfaces.Repositories;

public interface IUserRepository
{
    public Task CreateUser(User user);
    public Task<User?> GetUserById(Guid userId);
    public Task<bool> ExistsByEmail(string email);
    public Task<User?> GetUserByEmail(string email);
}