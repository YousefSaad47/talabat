using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specifications;
using Talabat.Repository.Data.Contexts;

namespace Talabat.Repository.Repositories;

public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
{
    private readonly StoreContext _dbContext;

    public GenericRepository(StoreContext dbContext) // Ask CLR for creating object from StoreContext implicitly
    {
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        if (typeof(TEntity) == typeof(Product))
        {
            return (IEnumerable<TEntity>) await _dbContext.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .ToListAsync();
        }
        
        return await _dbContext.Set<TEntity>().ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> GetAllWithSpecAsync(ISpecification<TEntity, TKey> spec)
    {
        return await ApplySpecification(spec).ToListAsync();
    }

    public async Task<TEntity?> GetByIdAsync(TKey id)
    {
        if (typeof(TEntity) == typeof(Product))
        {
            return await _dbContext.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id as int?) as TEntity;
        }
        
        return await _dbContext.Set<TEntity>().FindAsync(id);
    }

    public Task<TEntity?> GetWithSpecAsync(ISpecification<TEntity, TKey> spec)
    {
        return ApplySpecification(spec).FirstOrDefaultAsync();
    }

    public async Task AddAsync(TEntity entity)
    {
        await _dbContext.AddAsync(entity);
    }

    public void Update(TEntity entity)
    {
        _dbContext.Update(entity);
    }

    public void Delete(TEntity entity)
    {
        _dbContext.Remove(entity);
    }
    
    private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity, TKey> spec)
    {
        return SpecificationEvaluator<TEntity, TKey>.GetQuery(_dbContext.Set<TEntity>(), spec);
    }
}