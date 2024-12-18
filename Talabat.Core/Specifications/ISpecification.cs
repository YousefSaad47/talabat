using System.Linq.Expressions;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications;

public interface ISpecification<TEntity, TKey> where TEntity : BaseEntity<TKey>
{
    public Expression<Func<TEntity, bool>> Criteria { get; set; }

    public List<Expression<Func<TEntity, object>>> Includes { get; set; }

    public Expression<Func<TEntity, object>> OrderBy { get; set; }
    
    public Expression<Func<TEntity, object>> OrderByDesc { get; set; }

    public int Skip { get; set; }
    
    public int Take { get; set; }
    
    public bool IsPaginationEnabled { get; set; }
}