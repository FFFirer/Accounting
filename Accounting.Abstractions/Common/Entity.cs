using System.Diagnostics.Contracts;

namespace Accounting;

public abstract class Entity<Tkey>
{
    public Tkey Id { get; set; } = default!;
    public DateTimeOffset CreatedTime { get; set; }
    public DateTimeOffset LastModifiedTime { get; set; }
}