using MilkaHR.Application.Common.Interfaces;

namespace MilkaHR.Application.Candidate.Queries;

public record GetCandidateByIdQuery(int Id) : IRequest<MilkaHR.Domain.Entities.Candidate?>;

public class GetCandidateByIdQueryHandler(IApplicationDbContext db) : IRequestHandler<GetCandidateByIdQuery, MilkaHR.Domain.Entities.Candidate?>
{
    public async Task<MilkaHR.Domain.Entities.Candidate?> Handle(GetCandidateByIdQuery request, CancellationToken cancellationToken)
    {
        return await db
            .Candidates
            .Include(x => x.Cvs)
            .Include(x => x.Interviews)
            .Include(x => x.JobStatuses)
            .ThenInclude(x => x.Job)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
    }
}
