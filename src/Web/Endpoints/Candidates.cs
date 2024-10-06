using MilkaHR.Application.Candidate.Commands;
using MilkaHR.Application.Candidate.Queries;

namespace MilkaHR.Web.Endpoints;

public class Candidates : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapPost(AddCandidate)
            .MapPut(UpdateCandidate, "{id}")
            .MapDelete(RemoveCandidate, "{id}")
            .MapGet(GetCandidate, "{id}")
            .MapGet(GetAllCandidates)
            .MapGet(GetCandidatesCountsByJobs, "get-candidates-count-by-jobs")
            .MapGet("candidates-by-status-by-job", GetAllCandidatesByStatusByJob).Produces<Dictionary<string, List<int>>>();
    }

    private Task<Domain.Entities.Candidate> AddCandidate(ISender sender, AddCandidateCommand command)
    {
        return sender.Send(command);
    }

    private async Task<IResult> UpdateCandidate(ISender sender, int id, UpdateCandidateByIdCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }

    private async Task<IResult> RemoveCandidate(ISender sender, int id)
    {
        var isDeleted = await sender.Send(new RemoveCandidateCommand(id));
        return !isDeleted ? Results.NotFound() : Results.NoContent();
    }

    private async Task<IResult> GetCandidate(ISender sender, int id)
    {
        var candidate = await sender.Send(new GetCandidateByIdQuery(id));
        return candidate is null ? Results.NotFound() : Results.Ok(candidate);
    }

    private Task<IEnumerable<Domain.Entities.Candidate>> GetAllCandidates(ISender sender, [AsParameters] GetAllCandidatesQuery query)
    {
        return sender.Send(query);
    }
    
    private async Task<IResult> GetAllCandidatesByStatusByJob(ISender sender)
    {
        var statistics = await sender.Send(new GetAllCandidatesByStatusByJobQuery());
        return statistics is null ? Results.NotFound() : Results.Ok(statistics);
    }

    private async Task<IResult> GetCandidatesCountsByJobs(ISender sender)
    {
        var stats = await sender.Send(new GetCandidatesCountByJobQuery());
        return Results.Ok(stats);
    }
}
