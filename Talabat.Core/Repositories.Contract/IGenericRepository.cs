using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Core.Repositories.Contract;

public interface IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    
    Task<IEnumerable<TEntity>> GetAllWithSpecAsync(ISpecification<TEntity, TKey> spec);
    
    Task<TEntity?> GetByIdAsync(TKey id);
    
    Task<TEntity?> GetWithSpecAsync(ISpecification<TEntity, TKey> spec);
    
    Task AddAsync(TEntity entity);

    void Update(TEntity entity);
    
    void Delete(TEntity entity);
}