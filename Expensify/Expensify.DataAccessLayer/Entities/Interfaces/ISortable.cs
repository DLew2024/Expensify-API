namespace Expensify.Entities.Interfaces;

public interface ISortable : IIdentifiable
{
    public int? SortOrder { get; set; }
}