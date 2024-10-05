namespace MilkaHR.Domain.Entities;

public class CandidateJobProcessing : BaseAuditableEntity
{
    public required CandidateStatus ProcessingStatus { get; set; }
    public required Candidate Candidate { get; set; }
    public required Job Job { get; set; }
}
