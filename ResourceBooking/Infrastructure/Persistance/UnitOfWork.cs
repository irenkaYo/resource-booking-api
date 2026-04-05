using Microsoft.EntityFrameworkCore.Storage;
using Service.Interfaces;
using Service.Interfaces.Persistance;

namespace Infrastructure.Persistance;

public class UnitOfWork : IUnitOfWork
{
    private readonly ResourceBookingContext _context;
    private IDbContextTransaction _transaction;

    public UnitOfWork(ResourceBookingContext context)
    {
        _context = context;
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
        await _transaction.CommitAsync();
    }

    public async Task RollbackAsync()
    {
        await _transaction.RollbackAsync();
    }
}