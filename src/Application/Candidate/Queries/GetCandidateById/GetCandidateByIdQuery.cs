using MilkaHR.Application.Common.Interfaces;

namespace Microsoft.Extensions.DependencyInjection.Candidate.Queries.GetCandidateById;

public record GetCandidateById(int Id) : IRequest<MilkaHR.Domain.Entities.Candidate>;

public class GetCandidateByIdQueryHandler(IApplicationDbContext db) : IRequestHandler<GetCandidateById, MilkaHR.Domain.Entities.Candidate>
{
    public async Task<MilkaHR.Domain.Entities.Candidate> Handle(GetCandidateById request, CancellationToken cancellationToken)
    {
        return await db
            .Candidates
            .FirstAsync(x => x.Id == request.Id, cancellationToken);
    }
}
