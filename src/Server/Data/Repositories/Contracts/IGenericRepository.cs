namespace Server.Data.Repositories.Contracts;

public interface IGenericRepository<T>
{
    Task AddAsync(T item, CancellationToken ct = default);
    Task<T?> FindAsync(object id, CancellationToken ct = default);
    void Remove(T item);
    Task SaveChangesAsync(CancellationToken ct = default);
}
