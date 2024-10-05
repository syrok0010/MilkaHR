namespace MilkaHR.Domain.Entities;

public class Cv : BaseAuditableEntity
{
    public required Candidate Candidate { get; set; }
    public required HashSet<Job> Jobs { get; set; } = [];
}
