using MilkaHR.Application.Common.Models;
using MilkaHR.Application.Recruiter.Commands;
using MilkaHR.Application.Recruiter.Queries;
using MilkaHR.Domain.Entities;

namespace MilkaHR.Web.Endpoints;

public class Recruiter : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapPost(CreateRecruiter, "create-recruiter")
            .MapPut(UpdateRecruiter, "{id}")
            .MapDelete(DeleteRecruiter, "{id}")
            .MapGet(GetAllRecruiters)
            .MapGet(GetRecruiterById, "{id}")
            .MapPost(SetInterview)
            .MapPut(SetCandidateStatus, "set-status/{processingId}");
    }

    private Task<Domain.Entities.Recruiter> CreateRecruiter(ISender sender, CreateRecruiterCommand command)
    {
        return sender.Send(command);
    }

    private async Task<IResult> UpdateRecruiter(ISender sender, int id, UpdateRecruiterCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }

    private async Task<IResult> DeleteRecruiter(ISender sender, int id)
    {
        var recruiter = await sender.Send(new DeleteRecruiterCommand(id));
        if (recruiter is false) return Results.NotFound();
        return Results.NoContent();
    }

    private Task<IEnumerable<Domain.Entities.Recruiter>> GetAllRecruiters(ISender sender, [AsParameters] GetAllRecruitersQuery query)
    {
        return sender.Send(query);
    }

    private async Task<IResult> GetRecruiterById(ISender sender, int id)
    {
        var recruiter = await sender.Send(new GetRecruiterByIdQuery(id));
        return recruiter is null ? Results.NotFound() : Results.Ok(recruiter);
    }

    private Task<Interview> SetInterview(ISender sender, SetInterviewCommand command)
    {
        return sender.Send(command);
    }

    private async Task<IResult> SetCandidateStatus(ISender sender, int processingId, SetCandidateStatusCommand command)
    {
        if (processingId != command.ProcessingId) return Results.BadRequest();
        var processing = await sender.Send(command);
        return processing is null ? Results.NotFound() : Results.Ok(processing);
    }
}

