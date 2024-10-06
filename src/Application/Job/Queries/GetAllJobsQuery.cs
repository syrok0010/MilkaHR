using MilkaHR.Application.Common.Interfaces;
using MilkaHR.Domain.Enums;

namespace MilkaHR.Application.Job.Queries;

public record GetAllJobsQuery(
    string? Title = null,
    PriorityLevel? Priority = null,
    JobStatus? Status = null,
    JobCategory? Category = null
    ): IRequest<IEnumerable<Domain.Entities.Job>>;

public class GetAllJobsQueryHandler(IApplicationDbContext db)
    : IRequestHandler<GetAllJobsQuery, IEnumerable<Domain.Entities.Job>>
{
    private readonly IApplicationDbContext _db = db;
    
    public Task<IEnumerable<Domain.Entities.Job>> Handle(GetAllJobsQuery request, 
        CancellationToken cancellationToken)
    {
        var c = _db.Jobs
            .Include(x => x.CandidateStatuses)
            .ThenInclude(x => x.Candidate)
            .AsQueryable();

        if (request.Title is not null) 
            c = c.Where(x => x.Title == request.Title);
        
        if (request.Priority is not null) 
            c = c.Where(x => x.Priority == request.Priority);

        if (request.Status is not null) 
            c = c.Where(x => x.Status == request.Status);

        if (request.Category is not null) 
            c = c.Where(x => x.Category == request.Category);

        return Task.FromResult<IEnumerable<Domain.Entities.Job>>(c);
    }
}
