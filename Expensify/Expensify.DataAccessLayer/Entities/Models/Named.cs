using System.ComponentModel.DataAnnotations.Schema;
using Expensify.Entities.Interfaces;

namespace Expensify.DataAccessLater.Entities;

public abstract class Named : INamed
{
    protected Named(string name)
    {
        Name = name;
    }

    [Column(Order = 1)]
    public Guid Id { get; set; }

    [Column(Order = 2)]
    public string Name { get; set; }

    [Column("deletion_indicator")]
    public bool IsDeleted { get; set; }
    public Guid DeletedBy { get; set; }
    public long? DeletedAt { get; set; }
    public int? SortOrder { get; set; }
    public Guid LastUpdatedBy { get; set; }
    public Guid CreatedBy { get; set; }
    public long UpdatedDate { get; set; }
    public long CreateDate { get; set; }
}

