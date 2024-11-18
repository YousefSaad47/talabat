using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;

namespace Talabat.Core;

public interface IUnitOfWork
{
    Task<int> CompleteAsync();
    
    // Create Repository<T> and return

    IGenericRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : BaseEntity<TKey>;
}