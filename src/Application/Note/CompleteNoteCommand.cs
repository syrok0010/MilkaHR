using MilkaHR.Application.Common.Interfaces;

namespace MilkaHR.Application.Note;

public record CompleteNoteCommand(int Id) : IRequest<bool>;

public class CompleteNoteCommandHandler(IApplicationDbContext db)
    : IRequestHandler<CompleteNoteCommand, bool>
{
    private readonly IApplicationDbContext _db = db;

    public async Task<bool> Handle(CompleteNoteCommand request, CancellationToken cancellationToken)
    {
        var note = await _db.Notes.FirstOrDefaultAsync(n => n.Id == request.Id, cancellationToken);
        if (note is null)
            return false;
        note.Completed = true;
        await _db.SaveChangesAsync(cancellationToken);
        return true;
    }
}
