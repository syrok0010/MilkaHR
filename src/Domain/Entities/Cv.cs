namespace MilkaHR.Domain.Entities;

public class Cv : BaseAuditableEntity
{
    public required Candidate Candidate { get; set; }
    public required List<Job> Jobs { get; set; } = new();
}
