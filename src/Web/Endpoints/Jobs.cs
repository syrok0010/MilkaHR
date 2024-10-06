using MilkaHR.Application.Job.Commands;
using MilkaHR.Application.Job.Queries;
using MilkaHR.Domain.Entities;

namespace MilkaHR.Web.Endpoints;

public class Jobs : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapGet(GetJobsByMonthStats, "monthly-stats")
            .MapGet(GetJobsCountByPriority, "jobs-count-by-priority")
            .MapGet(GetAverageJobLifetime, "average-lifetime")
            .MapPost(CreateJob, "create-job")
            .MapPut(UpdateJob, "update-job");
    }

    private static Task<Dictionary<int, double>> GetJobsByMonthStats(ISender sender)
    {
        return sender.Send(new GetMonthlyAverageJobLifeTimeQuery());
    }
    
    private static Task<Dictionary<string, double>> GetAverageJobLifetime(ISender sender)
    {
        return sender.Send(new GetAverageJobLifeTimeQuery());
    }

    private static Task<List<StatisticByPriority>> GetJobsCountByPriority(ISender sender)
    {
        return sender.Send(new GetJobsCountByPriorityQuery());
    }

    private static Task<Job> CreateJob(ISender sender, CreateJobCommand command)
    {
        return sender.Send(command);
    }

    private static async Task<IResult> UpdateJob(ISender sender, int id, UpdateJobCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        var job = await sender.Send(command);
        return job is null ? Results.NotFound() : Results.Ok(job);
    }
}
