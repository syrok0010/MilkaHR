using MilkaHR.Application.Common.Models;
using MilkaHR.Application.Recruiter.Commands;
using MilkaHR.Application.Recruiter.Queries;

namespace MilkaHR.Web.Endpoints;

public class Recruiter : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapPost(CreateRecruiter)
            .MapPut(UpdateRecruiter, "{id}")
            .MapDelete(DeleteRecruiter, "{id}")
            .MapGet(GetAllRecruiters)
            .MapGet(GetRecruiterById, "{id}");
    }
    
    public Task<Domain.Entities.Recruiter> CreateRecruiter(ISender sender, CreateRecruiterCommand command)
    {
        return sender.Send(command);
    }
    
    public async Task<IResult> UpdateRecruiter(ISender sender, int id, UpdateRecruiterCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }
    
    public async Task<IResult> DeleteRecruiter(ISender sender, int id)
    {
        var recruiter = await sender.Send(new DeleteRecruiterCommand(id));
        if (recruiter is false) return Results.NotFound();
        return Results.NoContent();
    }
    
    public Task<IEnumerable<Domain.Entities.Recruiter>> GetAllRecruiters(ISender sender, [AsParameters] GetAllRecruitersQuery query)
    {
        return sender.Send(query);
    }
    
    public async Task<IResult> GetRecruiterById(ISender sender, int id)
    {
        var recruiter = await sender.Send(new GetRecruiterByIdQuery(id));
        return recruiter is null ? Results.NotFound() : Results.Ok(recruiter);
    }
}

