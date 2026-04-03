using Domain.Models;
using Infrastructure.EntityFrameworkRepository;
using Infrastructure.InterfacesRepositories;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private ResourceBookingContext db;

    public async Task CreateUser(User user)
    {
        await db.AddAsync(user);
        await db.SaveChangesAsync();
    }

    public async Task<User>? GetUserById(Guid userId)
    {
        User? user = await db.Users.FindAsync(userId);
        return user;
    }
}