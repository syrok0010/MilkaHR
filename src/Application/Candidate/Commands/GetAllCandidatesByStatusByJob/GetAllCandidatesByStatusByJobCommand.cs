using MilkaHR.Application.Common.Interfaces;
using MilkaHR.Domain.Enums;

namespace MilkaHR.Application.Candidate.Commands.GetAllCandidatesByStatusByJob;

public record GetAllCandidatesByStatusByJobQuery : IRequest<Dictionary<string, List<int>>?>;

public class GetAllCandidatesByStatusByJobCommand(IApplicationDbContext db) : IRequestHandler<GetAllCandidatesByStatusByJobQuery, Dictionary<string, List<int>>?>
{
    private readonly IApplicationDbContext _db = db;
    
    public async Task<Dictionary<string, List<int>>?> Handle(GetAllCandidatesByStatusByJobQuery request, CancellationToken cancellationToken)
    {
        var jobs = await _db.Jobs.Where(j => j.Status == JobStatus.Opened).ToListAsync(cancellationToken);
        if (jobs.Count == 0)
            return null;
        var numberOfCandidatesInEachOpenedJob = new Dictionary<string, List<int>>();
        foreach (var job in jobs)
        {
            numberOfCandidatesInEachOpenedJob.Add(job.Title, new List<int>());
            var processingStatuses = await _db.CandidateJobProcessings
                .Where(x => x.Job.Id == job.Id)
                .GroupBy(x => x.ProcessingStatus)
                .Select(g => new { ProcessingStatus = g.Key, Count = g.Count() })
                .ToListAsync(cancellationToken);
            if (processingStatuses.Count == 0)
                continue;
            foreach (var status in processingStatuses)
            {
                numberOfCandidatesInEachOpenedJob[job.Title].Add(status.Count);
            }
        }
        return numberOfCandidatesInEachOpenedJob.Count == 0 ? null : numberOfCandidatesInEachOpenedJob;
    }
}
