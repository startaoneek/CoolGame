using CoolGame.Server.DataAccess.Entities;
using CoolGame.Server.DataAccess.Entities.Interfaces;
using CoolGame.Server.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CoolGame.Server.DataAccess.Repositories;

internal class Repository<T> : IRepository<T> where T : class, IEntity
{
    protected CoolGameDbContext DbContext { get; }

    public Repository(CoolGameDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public Task<List<T>> ListAsync(CancellationToken cancellationToken)
        => DbContext.Set<T>().ToListAsync(cancellationToken);

    public void Add(T entity) => DbContext.Add(entity);

    public Task<T?> GetAsync(Guid id, CancellationToken cancellationToken)
        => DbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public void Update(T entity) => DbContext.Update(entity);

    public void Remove(T entity) => DbContext.Remove(entity);

    public void RemoveRange(ICollection<T> entities) => DbContext.RemoveRange(entities);
    
    public async Task SaveChangesAsync()
    {
        await DbContext.SaveChangesAsync();
    }
}