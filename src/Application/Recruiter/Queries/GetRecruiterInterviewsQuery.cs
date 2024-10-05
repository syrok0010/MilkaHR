using MilkaHR.Application.Common.Interfaces;
using MilkaHR.Domain.Entities;
using static System.DateTime;

namespace MilkaHR.Application.Recruiter.Queries;

public record GetRecruiterInterviewsQuery() : IRequest<List<Interview>?>;

public class GetRecruiterInterviewsByIdQueryHandler(IApplicationDbContext db)
    : IRequestHandler<GetRecruiterInterviewsQuery, List<Interview>?>
{
    private readonly IApplicationDbContext _db = db;
    
    public async Task<List<Interview>?> Handle(GetRecruiterInterviewsQuery request, 
        CancellationToken cancellationToken)
    {
        var interviews = await _db.Recruiters
            .Include(x => x.Interviews).ThenInclude(x => x.Job)
            .Include(x => x.Interviews).ThenInclude(x => x.Candidate)
            //.Where(r => r.Id == request.Id)
            .SelectMany(r => r.Interviews
                .Where(i => i.Timing.Date == Today.Date ||
                            i.Timing.Date == Today.Date.AddDays(1)))
            .ToListAsync(cancellationToken);
        return interviews.Count == 0 ? null : interviews;
    }
}

