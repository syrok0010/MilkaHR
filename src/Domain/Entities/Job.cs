namespace MilkaHR.Domain.Entities;

public class Job : BaseAuditableEntity
{
    public required string Title { get; set; }
    public required PriorityLevel Priority { get; set; }
    public required JobStatus Status { get; set; }
    public required DateTime PublicationDate { get; set; }
    public DateTime? ClosingDate { get; set; }
    public required Recruiter Recruiter { get; set; }
    public HashSet<CandidateJobProcessing> CandidateStatuses { get; set; } = [];
}
