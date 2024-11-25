using System.Linq.Expressions;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications;

public interface ISpecification<TEntity, TKey> where TEntity : BaseEntity<TKey>
{
    public Expression<Func<TEntity, bool>> Criteria { get; set; }

    public List<Expression<Func<TEntity, object>>> Includes { get; set; }
}