namespace Expensify.Entities.Interfaces;

public interface IAuditableEntity : IIdentifiable
{
    public string Name { get; set; }
}
