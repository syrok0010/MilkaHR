using Microsoft.Extensions.DependencyInjection.Candidate.Queries.GetCandidateById;
using MilkaHR.Application.Candidate.Commands.AddCandidate;
using MilkaHR.Application.Candidate.Commands.RemoveCandidate;
using MilkaHR.Application.Candidate.Commands.UpdateCandidateById;

namespace Microsoft.Extensions.DependencyInjection.Endpoints;

public class Candidates : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapPost(AddCandidate)
            .MapPut(UpdateCandidate, "{id}")
            .MapDelete(RemoveCandidate, "{id}")
            .MapGet(GetCandidate, "{id}");
    }

    public Task<MilkaHR.Domain.Entities.Candidate> AddCandidate(ISender sender, AddCandidateCommand command)
    {
        return sender.Send(command);
    }

    public async Task<IResult> UpdateCandidate(ISender sender, int id, UpdateCandidateByIdCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }

    public async Task<IResult> RemoveCandidate(ISender sender, int id)
    {
        await sender.Send(new RemoveCandidateCommand(id));
        return Results.NoContent();
    }

    public Task<MilkaHR.Domain.Entities.Candidate> GetCandidate(ISender sender, int id)
    {
        return sender.Send(new GetCandidateById(id));
    }
}
