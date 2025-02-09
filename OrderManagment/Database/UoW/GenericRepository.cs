using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace OrderManagment.Database.UoW;
public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, new()
{
    public GenericRepository(OrderDbContext context)
    {
        Context = context;
        DbSet = context.Set<TEntity>();
    }

    /// <summary>
    ///     Database context
    /// </summary>
    public OrderDbContext Context { get; }

    /// <summary>
    ///     DbSet
    /// </summary>
    public DbSet<TEntity> DbSet { get; }
    public async Task AddAsync(TEntity entity)
    {
        await DbSet.AddAsync(entity);
        await Context.SaveChangesAsync(); 
    }

    public IEnumerable<TEntity> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null)
    {
        var result = DbSet.Where(filter);
        return result.ToList();
    }

    public void Remove(TEntity entityToDelete)
    {
        if (Context.Entry(entityToDelete).State == EntityState.Detached)
            DbSet.Attach(entityToDelete);

        DbSet.Remove(entityToDelete);
    }

    public virtual void Update(TEntity entityToUpdate)
    {
        Context.Entry(entityToUpdate).State = EntityState.Detached;
        DbSet.Attach(entityToUpdate);
        Context.Entry(entityToUpdate).State = EntityState.Modified;
    }
    public virtual TEntity? GetById(params object[]? ids) => ids == null ? null : DbSet.Find(ids);

    public virtual bool Any(Expression<Func<TEntity, bool>> predicate) => DbSet.Any(predicate);
}
