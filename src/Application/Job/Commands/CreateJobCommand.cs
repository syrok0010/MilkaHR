using MilkaHR.Application.Common.Interfaces;
using MilkaHR.Domain.Enums;

namespace MilkaHR.Application.Job.Commands;

public record CreateJobCommand(
    string Title,
    PriorityLevel PriorityLevel,
    JobStatus Status,
    DateTime PublicationDate,
    JobCategory Category,
    int RecruiterId
) : IRequest<Domain.Entities.Job>;

public class CreateJobCommandHandler(IApplicationDbContext db) : IRequestHandler<CreateJobCommand, Domain.Entities.Job>
{
    private readonly IApplicationDbContext _db = db;
    
    public async Task<Domain.Entities.Job> Handle(CreateJobCommand request, CancellationToken cancellationToken)
    {
        var recruiter = await _db.Recruiters
            .FirstAsync(x => x.Id == request.RecruiterId, cancellationToken);
        var job = new Domain.Entities.Job
        {
            Category = request.Category,
            Title = request.Title,
            Priority = request.PriorityLevel,
            Status = request.Status,
            PublicationDate = request.PublicationDate,
            Recruiter = recruiter
        };
        recruiter.Jobs.Add(job);
        await _db.Jobs.AddAsync(job, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);
        return job;
    }
}
