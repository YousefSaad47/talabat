using System.Linq.Expressions;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications;

public class BaseSpecification<TEntity, TKey> : ISpecification<TEntity, TKey> where TEntity : BaseEntity<TKey>
{
    public Expression<Func<TEntity, bool>> Criteria { get; set; } = null;
    public List<Expression<Func<TEntity, object>>> Includes { get; set; } = new List<Expression<Func<TEntity, object>>>();

    public BaseSpecification(Expression<Func<TEntity, bool>> expression)
    {
        Criteria = expression;
    }

    public BaseSpecification()
    {
        
    }
}