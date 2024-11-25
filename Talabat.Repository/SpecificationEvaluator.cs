using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Repository;

public static class SpecificationEvaluator<TEntity, TKey> where TEntity : BaseEntity<TKey>
{
    public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity, TKey> spec)
    {
        var query = inputQuery;

        if (spec.Criteria is not null)
        {
            query = query.Where(spec.Criteria);
        }

        if (spec.OrderBy is not null)
        {
            query = query.OrderBy(spec.OrderBy);
        }
        
        if (spec.OrderByDesc is not null)
        {
            query = query.OrderByDescending(spec.OrderByDesc);
        }
      
        query = spec.Includes.Aggregate
        (query, (currentQuery, includeExp) 
            => currentQuery.Include(includeExp));

        if (spec.IsPaginationEnabled)
        {
            query = query.Skip(spec.Skip).Take(spec.Take);
        }
        
        return query;
    }
}