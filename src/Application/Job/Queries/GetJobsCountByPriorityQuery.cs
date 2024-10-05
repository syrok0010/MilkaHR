using MilkaHR.Application.Common.Interfaces;
using MilkaHR.Domain.Enums;

namespace MilkaHR.Application.Job.Queries;

public record GetJobsCountByPriorityQuery : IRequest<List<StatisticByPriority>>;

public class GetJobsCountByPriorityQueryHandler(IApplicationDbContext db)
    : IRequestHandler<GetJobsCountByPriorityQuery, List<StatisticByPriority>>
{
    private readonly IApplicationDbContext _db = db;
    
    public async Task<List<StatisticByPriority>> Handle(GetJobsCountByPriorityQuery request, CancellationToken cancellationToken)
    {
        var groupedJobs = await _db.Jobs.GroupBy(
            x => x.Priority,
            x => x,
            ((level, jobs) => new StatisticByPriority(
                level, 
                jobs
                    .Sum(x => x.CandidateStatuses
                    .Count(y => y.Job.Status == JobStatus.Closed)), 
                jobs.Sum(x => x.CandidateStatuses.Count)))).ToListAsync(cancellationToken);
        return groupedJobs;
    }
}
