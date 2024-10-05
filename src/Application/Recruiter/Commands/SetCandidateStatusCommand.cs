using MilkaHR.Application.Common.Interfaces;
using MilkaHR.Domain.Entities;
using MilkaHR.Domain.Enums;

namespace MilkaHR.Application.Recruiter.Commands;

public record SetCandidateStatusCommand(int ProcessingId, CandidateStatus NewStatus)
    : IRequest<CandidateJobProcessing?>;

public class SetCandidateStatusCommandHandler(IApplicationDbContext db) 
    : IRequestHandler<SetCandidateStatusCommand, CandidateJobProcessing?>
{
    private readonly IApplicationDbContext _db = db;

    public async Task<CandidateJobProcessing?> Handle(SetCandidateStatusCommand request, CancellationToken cancellationToken)
    {
        var processing = await _db.CandidateJobProcessings
            .FirstOrDefaultAsync(x => x.Id == request.ProcessingId, cancellationToken);

        if (processing is null)
        {
            return null;
        }
        
        processing.ProcessingStatus = request.NewStatus;
        await _db.SaveChangesAsync(cancellationToken);
        return processing;
    }
}
