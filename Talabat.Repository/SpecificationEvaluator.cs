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
        
        query = spec.Includes.Aggregate
        (query, (currentQuery, includeExp) 
            => currentQuery.Include(includeExp));
        
        return query;
    }
}