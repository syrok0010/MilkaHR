﻿using MilkaHR.Application.Common.Interfaces;
using MilkaHR.Domain.Enums;

namespace MilkaHR.Application.Candidate.Queries;

public record GetAllCandidatesQuery(
    int? AgeFrom = null,
    int? AgeTo = null,
    int? WorkExperience = null,
    string[]? Tags = null,
    string[]? JobTitles = null,
    CandidateStatus[]? Statuses = null): IRequest<IEnumerable<Domain.Entities.Candidate>>;

public class GetAllCandidatesQueryHandler(IApplicationDbContext db)
    : IRequestHandler<GetAllCandidatesQuery, IEnumerable<Domain.Entities.Candidate>>
{
    private readonly IApplicationDbContext _db = db;
    
    public Task<IEnumerable<Domain.Entities.Candidate>> Handle(GetAllCandidatesQuery request, 
        CancellationToken cancellationToken)
    {
        var c = _db.Candidates
            .Include(x => x.Cvs)
            .Include(x => x.Interviews)
            .Include(x => x.JobStatuses)
            .ThenInclude(x => x.Job)
            .AsQueryable();

        if (request.AgeFrom is not null) 
            c = c.Where(x => Math.Floor((DateTime.UtcNow - x.BirthDate).TotalDays / 365.25) >= request.AgeFrom);
        
        if (request.AgeTo is not null) 
            c = c.Where(x => Math.Floor((DateTime.UtcNow - x.BirthDate).TotalDays / 365.25) <= request.AgeTo);

        if (request.WorkExperience is not null) 
            c = c.Where(x => x.WorkExperience == request.WorkExperience);

        if (request.Tags is not null && request.Tags.Length > 0) 
            c = c.Where(x => request.Tags.All(t => x.Tags.Contains(t)));
        
        if (request.JobTitles is not null && request.JobTitles.Length > 0) 
            c = c.Where(x => request.JobTitles
                .All(t => x.JobStatuses.Any(j => j.Job.Title == t)));
        
        if (request.Statuses is not null && request.Statuses.Length > 0) 
            c = c.Where(x => request.Statuses
                .All(t => x.JobStatuses.Any(j => j.ProcessingStatus == t)));

        return Task.FromResult<IEnumerable<Domain.Entities.Candidate>>(c);
    }
}
