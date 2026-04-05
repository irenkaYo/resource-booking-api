using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
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
        var connection = _context.Database.GetDbConnection();

        if (connection.State != ConnectionState.Open)
            await connection.OpenAsync();

        var dbTransaction = await connection.BeginTransactionAsync(IsolationLevel.Serializable);

        _transaction = _context.Database.UseTransaction(dbTransaction);
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