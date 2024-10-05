using MilkaHR.Application.Common.Interfaces;
using MilkaHR.Domain.Enums;

namespace MilkaHR.Application.Job.Queries;

public record GetJobsCountByPriorityQuery : IRequest<List<StatisticByPriority>>;

public class GetJobsCountByPriorityQueryHandler(IApplicationDbContext db) : IRequestHandler<GetJobsCountByPriorityQuery, List<StatisticByPriority>>
{
    private readonly IApplicationDbContext _db = db;
    
    public Task<List<StatisticByPriority>> Handle(GetJobsCountByPriorityQuery request, CancellationToken cancellationToken)
    {
        return _db.Jobs
            .GroupBy(
                x => x.Priority,
                x => x,
                (level, jobs) => new StatisticByPriority(
                        level, 
                        jobs.Count(x => x.Status == JobStatus.Opened), 
                        jobs.Count()
                )
            ).ToListAsync(cancellationToken);
    }
}
