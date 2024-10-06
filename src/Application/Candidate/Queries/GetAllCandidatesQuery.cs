using MilkaHR.Application.Common.Interfaces;
using MilkaHR.Application.Recruiter.Queries;

namespace MilkaHR.Application.Candidate.Queries;

public record GetAllCandidatesQuery: IRequest<IEnumerable<Domain.Entities.Candidate>>;

public class GetAllCandidatesQueryHandler(IApplicationDbContext db)
    : IRequestHandler<GetAllCandidatesQuery, IEnumerable<Domain.Entities.Candidate>>
{
    private readonly IApplicationDbContext _db = db;
    
    public Task<IEnumerable<Domain.Entities.Candidate>> Handle(GetAllCandidatesQuery request, 
        CancellationToken cancellationToken)
    {
        return Task.FromResult<IEnumerable<Domain.Entities.Candidate>>(_db.Candidates
            .Include(x => x.Cvs)
            .Include(x => x.Interviews)
            .Include(x => x.JobStatuses));
    }
}
