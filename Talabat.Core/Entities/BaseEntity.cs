namespace Talabat.Core.Entities;

public class BaseEntity<TKey>
{
    public TKey Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}