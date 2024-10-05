using MilkaHR.Application.Common.Interfaces;
using MilkaHR.Domain.Enums;

namespace Microsoft.Extensions.DependencyInjection.Candidate.Commands.GetAllCandidatesByStatusByJob;

public record GetAllCandidatesByStatusByJob(int JobId) : IRequest<List<Statistics>>;

public class GetAllCandidatesByStatusByJobCommand(IApplicationDbContext db) : IRequestHandler<GetAllCandidatesByStatusByJob, List<Statistics>>
{
    private readonly IApplicationDbContext _db = db;
    
    public async Task<List<Statistics>> Handle(GetAllCandidatesByStatusByJob request, CancellationToken cancellationToken)
    {
        var job = await _db.Jobs.FirstOrDefaultAsync(x => x.Id == request.JobId, cancellationToken: cancellationToken);
        return [new Statistics(), new Statistics(), new Statistics()];
    }
}
