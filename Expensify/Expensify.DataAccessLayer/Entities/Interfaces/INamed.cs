namespace Expensify.Entities.Interfaces;

public interface INamed : ISortable
{
    public string Name { get; set; }
}

