using System.ComponentModel.DataAnnotations.Schema;
using Expensify.Entities.Interfaces;

namespace Expensify.DataAccessLayer.Entities.AbstractClasses;

public abstract class Auditable : IAuditableEntity
{
    protected Auditable()
    {
        Id = Guid.NewGuid();
        CreateDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        UpdatedDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    }

    protected Auditable(string name)
        : this()
    {
        Id = Guid.NewGuid();
        Name = name;
        CreateDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        UpdatedDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    }

    [Column(Order = 1)]
    public Guid Id { get; set; }

    [Column(Order = 2)]
    public string Name { get; set; } = string.Empty;

    [Column("deletion_indicator")]
    public bool IsDeleted { get; set; }
    public Guid LastUpdatedBy { get; set; }
    public Guid CreatedBy { get; set; }
    public long UpdatedDate { get; set; }
    public long CreateDate { get; set; }
}
