using Talabat.Core.Entities;

namespace Talabat.Core.Repositories.Contract;

public interface IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    
    Task<TEntity?> GetByIdAsync(TKey id);
    
    Task AddAsync(TEntity entity);

    void Update(TEntity entity);
    
    void Delete(TEntity entity);
}