namespace OrderManagment.Database;

/// <summary>
///     Transaction database
/// </summary>
public interface IDbTransaction : IDisposable
{
    /// <summary>
    ///     Begin transaction
    /// </summary>
    Task BeginTransaction();

    /// <summary>
    ///     Commit transaction
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Execution result</returns>
    Task CommitAsync(CancellationToken cancellationToken = default);

    /// <summary>
    ///     Rollback transaction
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Execution result</returns>
    Task RollbackAsync(CancellationToken cancellationToken = default);
}