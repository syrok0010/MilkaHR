﻿using MilkaHR.Domain.Entities;

namespace MilkaHR.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Domain.Entities.Candidate> Candidates { get; }

    DbSet<Cv> Cvs { get; }
    
    DbSet<Domain.Entities.Job> Jobs { get; }
    
    DbSet<Domain.Entities.Recruiter> Recruiters { get; }
    
    DbSet<CandidateJobProcessing> CandidateJobProcessings { get; }
    
    DbSet<Interview> Interviews { get; }
    
    DbSet<Domain.Entities.Note> Notes { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
