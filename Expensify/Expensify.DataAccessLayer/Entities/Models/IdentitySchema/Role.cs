namespace Expensify.DataAccessLayer.Entities.Models.IdentitySchema;

public class Role
{
    /// <summary>
    /// The unique identifier for the role.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The name of the role.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// The collection of users assigned to this role.
    /// </summary>
    public ICollection<User> Users { get; set; } = [];
}
