using MilkaHR.Application.Job.Queries;

namespace MilkaHR.Web.Endpoints;

public class Jobs : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapGet(GetJobsByMonthStats, "monthly-stats")
            .MapGet(GetJobsCountByPriority, "jobs-count-by-priority")
            .MapGet(GetAverageJobLifetime, "average-lifetime");
    }

    private static Task<Dictionary<int, double>> GetJobsByMonthStats(ISender sender)
    {
        return sender.Send(new GetMonthlyAverageJobLifeTimeQuery());
    }
    
    private static Task<double> GetAverageJobLifetime(ISender sender)
    {
        return sender.Send(new GetAverageJobLifeTimeQuery());
    }

    private static Task<List<StatisticByPriority>> GetJobsCountByPriority(ISender sender)
    {
        return sender.Send(new GetJobsCountByPriorityQuery());
    }
}
