using MilkaHR.Application.Common.Interfaces;

namespace MilkaHR.Application.Note;

public record CreateNoteCommand(string text) : IRequest<Domain.Entities.Note>;

public class CreateNoteCommandHandler(IApplicationDbContext db) : IRequestHandler<CreateNoteCommand, Domain.Entities.Note>
{
    private readonly IApplicationDbContext _db = db;
    public async Task<Domain.Entities.Note> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
    {
        var note = new Domain.Entities.Note
        {
            Text = request.text,
            Completed = false
        };
        await _db.Notes.AddAsync(note, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);
        return note;
    }
}
