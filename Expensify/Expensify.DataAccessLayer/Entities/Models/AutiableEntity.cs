using System.ComponentModel.DataAnnotations.Schema;
using Expensify.Entities.Interfaces;

namespace Expensify.DataAccessLayer.Entities.Models;

/// <summary>
/// Base entity that provides common auditing, soft deletion, and naming
/// functionality for entities within the application.
/// </summary>
public abstract class AuditableEntity : IAuditableEntity
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AuditableEntity"/> class.
    /// </summary>
    /// <param name="name">The display name of the entity.</param>
    protected AuditableEntity(string name)
    {
        Name = name;
    }

    /// <summary>
    /// Unique identifier for the entity.
    /// </summary>
    [Column(Order = 1)]
    public Guid Id { get; set; }

    /// <summary>
    /// Display name of the entity.
    /// </summary>
    [Column(Order = 2)]
    public string Name { get; set; }

    /// <summary>
    /// Indicates whether the entity has been soft deleted.
    /// Soft deleted entities remain in the database but are excluded from normal queries.
    /// </summary>
    [Column("deletion_indicator")]
    public bool IsDeleted { get; set; }

    /// <summary>
    /// The user who soft deleted the entity.
    /// </summary>
    public Guid? DeletedBy { get; set; }

    /// <summary>
    /// The date and time the entity was soft deleted.
    /// Stored as a long timestamp.
    /// </summary>
    public long? DeletedAt { get; set; }

    /// <summary>
    /// Optional sort order used when displaying collections of entities.
    /// Lower values are displayed first.
    /// </summary>
    public int? SortOrder { get; set; }

    /// <summary>
    /// The user who last updated the entity.
    /// Null if the entity has never been modified.
    /// </summary>
    public Guid? LastUpdatedBy { get; set; }

    /// <summary>
    /// The user who created the entity.
    /// </summary>
    public Guid CreatedBy { get; set; }

    /// <summary>
    /// The date and time the entity was last updated.
    /// Null if the entity has never been modified.
    /// Stored as a long timestamp.
    /// </summary>
    public long? UpdatedDate { get; set; }

    /// <summary>
    /// The date and time the entity was created.
    /// Stored as a long timestamp.
    /// </summary>
    public long CreateDate { get; set; }
}
