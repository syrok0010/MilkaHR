using MilkaHR.Application.Common.Interfaces;
using MilkaHR.Domain.Enums;

namespace MilkaHR.Application.Candidate.Queries;

public record GetCandidatesCountByJobQuery(int Months) : IRequest<List<int>>;

public class GetCandidatesCountByJobQueryHandler(IApplicationDbContext db) :
    IRequestHandler<GetCandidatesCountByJobQuery, List<int>>
{
    private readonly IApplicationDbContext _db = db;
    public async Task<List<int>> Handle(GetCandidatesCountByJobQuery request, CancellationToken cancellationToken)
    {
        var jobs = _db.Jobs.Where(x => x.Status == JobStatus.Opened &&
                                       x.PublicationDate >= DateTime.UtcNow.AddMonths(-request.Months));
        var counts = new List<int>();
        foreach (var job in jobs)
        {
            var count = await _db.Candidates.CountAsync(x => x.JobStatuses.Any(y => y.Job.Id == job.Id), cancellationToken);
            counts.Add(count);
        }

        return counts;
    }
}
