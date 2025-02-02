using Microsoft.EntityFrameworkCore;
using Server.Data.Repositories.Contracts;

namespace Server.Data.Repositories.Implementations;

public class GenericRepository<T>(DbContext context) : IGenericRepository<T> where T : class
{
    private readonly DbSet<T> _entities = context.Set<T>();

    public async Task AddAsync(T item, CancellationToken ct = default)
    {
        await _entities.AddAsync(item, ct);
    }

    public async Task<T?> FindAsync(object id, CancellationToken ct = default)
    {
        return await _entities.FindAsync([id], cancellationToken: ct);
    }

    public void Remove(T item)
    {
        _entities.Remove(item);
    }

    public async Task SaveChangesAsync(CancellationToken ct = default)
    {
        await context.SaveChangesAsync(ct);
    }
}