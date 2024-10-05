using MilkaHR.Application.Common.Interfaces;
using MilkaHR.Domain.Entities;
using MilkaHR.Domain.Enums;

namespace MilkaHR.Application.Recruiter.Commands;

public record SetInterviewCommand(int CandidateId, int JobId, DateTime Timing, EventType Type) : IRequest<Interview>;

public class SetInterviewCommandHandler(IApplicationDbContext db) : IRequestHandler<SetInterviewCommand, Interview>
{
    private readonly IApplicationDbContext _db = db;
    
    public async Task<Interview> Handle(SetInterviewCommand request, CancellationToken cancellationToken)
    {
        var candidate = await _db.Candidates.FirstAsync(x => x.Id == request.CandidateId, cancellationToken);
        var job = await _db.Jobs.FirstAsync(x => x.Id == request.JobId, cancellationToken);
        var interview = new Interview { Candidate = candidate, Job = job, Timing = request.Timing, Type = request.Type };
        candidate.Interviews.Add(interview);
        await _db.Interviews.AddAsync(interview, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);
        return interview;
    }
}
