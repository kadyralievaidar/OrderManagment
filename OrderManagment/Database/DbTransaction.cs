using Microsoft.EntityFrameworkCore.Storage;

namespace OrderManagment.Database;

/// <summary>
///     Transaction database
/// </summary>
public class DbTransaction(OrderDbContext context) : IDbTransaction
{
    private readonly OrderDbContext _context = context;
    private IDbContextTransaction? _transaction;
    private long _countActiveTransaction;

    /// <summary>
    ///     Begin transaction
    /// </summary>
    /// <param name="isolationLevel">Isolation level</param>
    public async Task BeginTransaction()
    {
        if (Interlocked.Read(ref _countActiveTransaction) == 0)
            _transaction = await _context.Database.BeginTransactionAsync(CancellationToken.None);

        Interlocked.Increment(ref _countActiveTransaction);
    }

    /// <summary>
    ///     Commit transaction
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Execution result</returns>
    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction is null)
            throw new InvalidOperationException();

        Interlocked.Decrement(ref _countActiveTransaction);

        if (Interlocked.Read(ref _countActiveTransaction) == 0)
            await _transaction.CommitAsync(cancellationToken);
    }

    /// <summary>
    ///     Rollback transaction
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Execution result</returns>
    public async Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction is null)
            throw new InvalidOperationException();

        Interlocked.Decrement(ref _countActiveTransaction);

        if (Interlocked.Read(ref _countActiveTransaction) <= 0)
            await _transaction.RollbackAsync(cancellationToken);
    }

    ~DbTransaction()
    {
        Dispose(false);
    }

    private void Dispose(bool disposing)
    {
        if (disposing && Interlocked.Read(ref _countActiveTransaction) <= 0)
            _transaction?.Dispose();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}