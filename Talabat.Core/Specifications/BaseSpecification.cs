using System.Linq.Expressions;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications;

public class BaseSpecification<TEntity, TKey> : ISpecification<TEntity, TKey> where TEntity : BaseEntity<TKey>
{
    public Expression<Func<TEntity, bool>> Criteria { get; set; } = null;
    
    public List<Expression<Func<TEntity, object>>> Includes { get; set; } = new List<Expression<Func<TEntity, object>>>();
    
    public Expression<Func<TEntity, object>> OrderBy { get; set; }
    
    public Expression<Func<TEntity, object>> OrderByDesc { get; set; }
    
    public int Skip { get; set; }
    
    public int Take { get; set; }
    
    public bool IsPaginationEnabled { get; set; }

    public BaseSpecification(Expression<Func<TEntity, bool>> expression)
    {
        Criteria = expression;
    }

    public BaseSpecification()
    {
        
    }

    public void AddOrderBy(Expression<Func<TEntity, object>> expression)
    {
        OrderBy = expression;
    }
    
    public void AddOrderByDesc(Expression<Func<TEntity, object>> expression)
    {
        OrderByDesc = expression;
    }

    public void ApplyPagination(int skip, int take)
    {
        IsPaginationEnabled = true;
        Skip = skip;
        Take = take;
    }
}