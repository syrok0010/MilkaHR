namespace MilkaHR.Domain.Entities;

public class Candidate : BaseAuditableEntity
{
    public required string Name { get; set; }
    public required string LastName { get; set; }
    public required string MiddleName { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }
    public required string Address { get; set; }
    public required int SalaryPreference { get; set; } 
    public required List<Cv> Cvs { get; set; } = [];
    public required List<CandidateJobProcessing> JobStatuses { get; set; } = [];
    public List<Interview> Interviews { get; set; } = [];
}
