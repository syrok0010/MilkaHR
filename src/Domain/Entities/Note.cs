namespace MilkaHR.Domain.Entities;

public class Note : BaseAuditableEntity
{
    public required string Text { get; set; }
    public required bool Completed { get; set; }
}
