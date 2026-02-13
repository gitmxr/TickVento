using Microsoft.EntityFrameworkCore.Storage;
using TickVento.Application.Abstractions.Persistence;
using TickVento.Infrastructure.Persistence.Data;

namespace TickVento.Infrastructure.Persistence.UnitOfWork;

public  class UnitOfWork(
    TickVentoDbContext dbContext) : IUnitOfWork
{
    private readonly TickVentoDbContext _dbContext = dbContext;
    private IDbContextTransaction? _currentTransaction;

    public async Task BeginAsync()
    {
        if (_currentTransaction != null)
            return; 
        _currentTransaction = await _dbContext.Database.BeginTransactionAsync();
    }

    public async Task CommitAsync()
    {
        if( _currentTransaction == null)
            throw new InvalidOperationException("No transaction started.");

        try
        {
            await _dbContext.SaveChangesAsync();
            await _currentTransaction.CommitAsync();
        }
        catch
        {
            await RollbackAsync();
            throw;
        }
        finally
        {
            await _currentTransaction.DisposeAsync();
            _currentTransaction = null;
        }
    }
    public async Task RollbackAsync()
    {
        if (_currentTransaction == null)
            return;

            await _currentTransaction.RollbackAsync();
            await _currentTransaction.DisposeAsync();
            _currentTransaction = null;

    }

}
