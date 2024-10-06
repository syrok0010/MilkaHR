using MilkaHR.Application.Common.Interfaces;
using MilkaHR.Domain.Enums;

namespace MilkaHR.Application.Job.Commands;

public record UpdateJobCommand(
    int Id,
    string Title,
    PriorityLevel PriorityLevel,
    JobStatus Status,
    int RecruiterId,
    JobCategory Category,
    DateTime? ClosingDate,
    string Description
) : IRequest<Domain.Entities.Job?>;

public class UpdateJobCommandHandler(IApplicationDbContext db) :
    IRequestHandler<UpdateJobCommand, Domain.Entities.Job?>
{
    private readonly IApplicationDbContext _db = db;
    
    public async Task<Domain.Entities.Job?> Handle(UpdateJobCommand request, CancellationToken cancellationToken)
    {
        var job = await _db.Jobs.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (job is null)
        {
            return null;
        }

        job.Title = request.Title;
        job.Priority = request.PriorityLevel;
        job.Status = request.Status;
        job.Recruiter = await _db.Recruiters.FirstAsync(x => x.Id == request.RecruiterId, cancellationToken);
        job.Category = request.Category;
        job.ClosingDate = request.ClosingDate;
        job.Description = request.Description;

        await _db.SaveChangesAsync(cancellationToken);
        return job;
    }
}
