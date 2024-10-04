using MilkaHR.Application.Common.Models;
using MilkaHR.Application.Recruiter.Commands;
//using MilkaHR.Application.Recruiter.Queries.GetRecruitersWithPagination;

namespace MilkaHR.Web.Endpoints;

public class Recruiter : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            //.RequireAuthorization()
            //.MapGet(GetRecruiters)
            .MapPost(CreateRecruiter);
        //.MapPut(UpdateRecruiter, "{id}")
        //.MapDelete(DeleteRecruiter, "{id}");
    }
    
    public Task<Domain.Entities.Recruiter> CreateRecruiter(ISender sender, CreateRecruiterCommand command)
    {
        return sender.Send(command);
    }

    // public Task<IEnumerable<Domain.Entities.Recruiter>> GetRecruiters(ISender sender, [AsParameters] GetRecruitersQuery query)
    // {
    //     return sender.Send(query);
    // }
    //
    // public async Task<IResult> UpdateRecruiter(ISender sender, int id, UpdateRecruiterCommand command)
    // {
    //     if (id != command.Id) return Results.BadRequest();
    //     await sender.Send(command);
    //     return Results.NoContent();
    // }
    //
    // public async Task<IResult> DeleteRecruiter(ISender sender, int id)
    // {
    //     await sender.Send(new DeleteRecruiterCommand(id));
    //     return Results.NoContent();
    // }
}

