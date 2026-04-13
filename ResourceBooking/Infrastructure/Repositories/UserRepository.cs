using Domain.Models;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;
using Service.Interfaces.Repositories;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ResourceBookingContext db;

    public UserRepository(ResourceBookingContext context)
    {
        db = context;
    }
    public async Task CreateUser(User user)
    {
        await db.AddAsync(user);
        await db.SaveChangesAsync();
    }

    public async Task<User?> GetUserById(Guid userId)
    {
        User? user = await db.Users
            .Include(x => x.Bookings)
            .FirstOrDefaultAsync(x => x.Id == userId);
        return user;
    }
    
    public async Task<bool> ExistsByEmail(string email)
    {
        return await db.Users.AnyAsync(u => u.Email == email);
    }
    
    public async Task<User>? GetUserByEmail(string email)
    {
        User? user = await db.Users
            .Include(x => x.Bookings)
            .FirstOrDefaultAsync(u => u.Email == email);
        return user;
    }
}