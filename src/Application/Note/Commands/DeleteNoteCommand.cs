using MilkaHR.Application.Common.Interfaces;

namespace MilkaHR.Application.Note;

public record DeleteNoteCommand(int Id) : IRequest<bool>;

public class DeleteNoteCommandHandler(IApplicationDbContext db)
    : IRequestHandler<DeleteNoteCommand, bool>
{
    private readonly IApplicationDbContext _db = db;

    public async Task<bool> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
    {
        var note = await _db.Notes.FirstOrDefaultAsync(n => n.Id == request.Id, cancellationToken);
        if (note is null)
            return false;
        _db.Notes.Remove(note);
        await _db.SaveChangesAsync(cancellationToken);
        return true;
    }
}
