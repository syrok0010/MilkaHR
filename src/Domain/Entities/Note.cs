namespace MilkaHR.Domain.Entities;

public class Note : BaseAuditableEntity
{
    public required string Text { get; set; }
}
