using MilkaHR.Application.Common.Interfaces;

namespace MilkaHR.Application.Candidate.Commands.RemoveCandidate;

public record RemoveCandidateCommand(int Id) : IRequest<bool>;

public class RemoveCandidateCommandHandler(IApplicationDbContext db) : IRequestHandler<RemoveCandidateCommand, bool>
{
    public async Task<bool> Handle(RemoveCandidateCommand request, CancellationToken cancellationToken)
    {
        var candidate = await db.Candidates.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (candidate is null)
        {
            return false;
        }
        db.Candidates.Remove(candidate);
        await db.SaveChangesAsync(cancellationToken);
        return true;
    }
}
