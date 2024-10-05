using MilkaHR.Application.Job.Queries;

namespace MilkaHR.Web.Endpoints;

public class Jobs : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapGet(GetJobsByMonthStats, "monthly-stats");
    }

    private static Task<Dictionary<int, double>> GetJobsByMonthStats(ISender sender)
    {
        return sender.Send(new GetMonthlyAverageJobLifeTimeQuery());
    }
}
