using MilkaHR.Application.Common.Interfaces;

namespace MilkaHR.Application.Candidate.Commands.RemoveCandidate;

public record RemoveCandidateCommand(int Id) : IRequest;

public class RemoveCandidateCommandHandler(IApplicationDbContext db) : IRequestHandler<RemoveCandidateCommand>
{
    public async Task Handle(RemoveCandidateCommand request, CancellationToken cancellationToken)
    {
        var candidate = await db.Candidates.FirstAsync(x => x.Id == request.Id, cancellationToken);
        db.Candidates.Remove(candidate);
        await db.SaveChangesAsync(cancellationToken);
    }
}
