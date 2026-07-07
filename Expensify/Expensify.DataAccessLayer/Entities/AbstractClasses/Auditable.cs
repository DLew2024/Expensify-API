using Expensify.Entities.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Expensify.DataAccessLayer.Entities.AbstractClasses;

public abstract class Auditable : IAuditableEntity
{
    protected Auditable()
    {
        Id = Guid.NewGuid();
        CreateDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    }

    protected Auditable(string name) : this()
    {
        Name = name;
    }

    [Column(Order = 1)]
    public Guid Id { get; set; }

    [Column(Order = 2)]
    public string Name { get; set; }

    [Column("deletion_indicator")]
    public bool IsDeleted { get; set; }
    public Guid LastUpdatedBy { get; set; }
    public Guid CreatedBy { get; set; }
    public long UpdatedDate { get; set; }
    public long CreateDate { get; set; }
}
