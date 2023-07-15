using CoolGame.Server.DataAccess.Entities.Interfaces;

namespace CoolGame.Server.DataAccess.Repositories.Interfaces;


public interface IRepository
{
}

public interface IRepository<T> : IRepository
    where T : IEntity
{
    Task<List<T>> ListAsync(CancellationToken cancellationToken = default);
    void Add(T entity);
    void Remove(T entity);
    Task<T?> GetAsync(Guid id, CancellationToken cancellationToken = default);
    void Update(T entity);

    Task SaveChangesAsync();
}