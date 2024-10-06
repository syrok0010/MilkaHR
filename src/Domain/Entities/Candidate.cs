namespace MilkaHR.Domain.Entities;

public class Candidate : BaseAuditableEntity
{ 
    public required string Name { get; set; }
    public required string LastName { get; set; }
    public required string MiddleName { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }
    public required string Address { get; set; }
    public required HashSet<Cv> Cvs { get; set; } = [];
    
    public string? Photo { get; set; }
    
    public required DateTime BirthDate { get; set; }
    
    public required int WorkExperience { get; set; }
    
    public required string LastJob { get; set; }
    
    public required List<string> Tags { get; set; }
    
    public required string Education { get; set; }
    public required HashSet<CandidateJobProcessing> JobStatuses { get; set; } = [];
    public HashSet<Interview> Interviews { get; set; } = [];
}
