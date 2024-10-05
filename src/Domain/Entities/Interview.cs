namespace MilkaHR.Domain.Entities;

public class Interview : BaseAuditableEntity
{
    public required DateTime Timing { get; set; }
    public required Job Job { get; set; }
    public required Candidate Candidate { get; set; }
}
