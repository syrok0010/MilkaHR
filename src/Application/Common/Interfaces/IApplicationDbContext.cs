using MilkaHR.Domain.Entities;

namespace MilkaHR.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Candidate> Candidates { get; }

    DbSet<Cv> Cvs { get; }
    
    DbSet<Job> Jobs { get; }
    
    DbSet<Domain.Entities.Recruiter> Recruiters { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
