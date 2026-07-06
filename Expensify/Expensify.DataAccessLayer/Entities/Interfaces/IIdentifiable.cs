namespace Expensify.Entities.Interfaces;

public interface IIdentifiable
{
    Guid Id { get; set; }
    public Guid LastUpdatedBy { get; set; }
    public Guid CreatedBy { get; set; }
    public long UpdatedDate { get; set; }
    public long CreateDate { get; set; }
}
