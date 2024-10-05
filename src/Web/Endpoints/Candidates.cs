using MilkaHR.Application.Candidate.Commands.AddCandidate;
using MilkaHR.Application.Candidate.Commands.GetAllCandidatesByStatusByJob;
using MilkaHR.Application.Candidate.Commands.GetAllCandidatesForJob;
using MilkaHR.Application.Candidate.Commands.RemoveCandidate;
using MilkaHR.Application.Candidate.Commands.UpdateCandidateById;
using MilkaHR.Application.Candidate.Queries.GetCandidateById;

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
            .MapGet(GetAllCandidatesByStatusByJob, "candidates-by-status-by-job")
            .MapGet(GetCandidatesCountsByJobs, "get-candidates-count-by-jobs");
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
        var candidate = await sender.Send(new GetCandidateById(id));
        return candidate is null ? Results.NotFound() : Results.Ok(candidate);
    }

    private async Task<IResult> GetAllCandidatesByStatusByJob(ISender sender)
    {
        var statistics = await sender.Send(new GetAllCandidatesByStatusByJobQuery());
        return statistics is null ? Results.NotFound() : Results.Ok(statistics);
    }

    private async Task<IResult> GetCandidatesCountsByJobs(ISender sender)
    {
        var stats = await sender.Send(new GetCandidatesCountByJobCommand());
        return Results.Ok(stats);
    }
}
