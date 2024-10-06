using MilkaHR.Application.Note;
using MilkaHR.Application.Note.Queries;
using MilkaHR.Application.Recruiter.Commands;
using MilkaHR.Application.Recruiter.Queries;
using MilkaHR.Domain.Entities;

namespace MilkaHR.Web.Endpoints;

public class Recruiter : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            //.RequireAuthorization()
            .MapPost(CreateRecruiter, "create-recruiter")
            .MapPut(UpdateRecruiter, "{id}")
            .MapDelete(DeleteRecruiter, "{id}")
            .MapGet(GetAllRecruiters)
            .MapGet(GetRecruiterById, "{id}")
            .MapPost(SetInterview)
            .MapPut(SetCandidateStatus, "set-status/{processingId}")
            .MapDelete(DeleteNote, "note/{id}")
            .MapPost(CreateNote, "create-note")
            .MapPut(CompleteNote, "complete-note")
            .MapGet(GetAllNotes, "get-notes")
            .MapGet(GetRecruiterInterviews, "interviews");
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
        return recruiter is false ? Results.NotFound() : Results.NoContent();
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
    
    private async Task<List<Interview>?> GetRecruiterInterviews(ISender sender)
    {
        return await sender.Send(new GetRecruiterInterviewsQuery());
        //return interviews is null ? Results.NotFound() : Results.Ok(interviews);
    }
    
    private async Task<IResult> DeleteNote(ISender sender, int id)
    {
        var note = await sender.Send(new DeleteNoteCommand(id));
        return note is false ? Results.NotFound() : Results.NoContent();
    }

    private Task<Note> CreateNote(ISender sender, CreateNoteCommand command)
    {
        return sender.Send(command);
    }
    
    private async Task<IResult> CompleteNote(ISender sender, int id)
    {
        var note = await sender.Send(new CompleteNoteCommand(id));
        return note is false ? Results.NotFound() : Results.NoContent();
    }
    
    private Task<IEnumerable<Note>> GetAllNotes(ISender sender, [AsParameters] GetAllNotesQuery query)
    {
        return sender.Send(query);
    }

}

