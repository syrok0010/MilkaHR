using MilkaHR.Application.Common.Interfaces;
using MilkaHR.Domain.Entities;

namespace MilkaHR.Application.Recruiter.Commands;

public record UpdateRecruiterCommand(int Id, string Name, string LastName, string MiddleName, string Email,
    string Phone, byte WorkExperience, List<Domain.Entities.Job> Jobs, List<Interview> Interviews)
    : IRequest<Domain.Entities.Recruiter>;

public class UpdateRecruiterCommandHandler(IApplicationDbContext db)
    : IRequestHandler<UpdateRecruiterCommand, Domain.Entities.Recruiter>
{
    private readonly IApplicationDbContext _db = db;

    public async Task<Domain.Entities.Recruiter> Handle(UpdateRecruiterCommand request, CancellationToken cancellationToken)
    {
        var recruiter = await _db.Recruiters
            .Include(x => x.Jobs)
            .FirstAsync(r => r.Id == request.Id, cancellationToken);
        recruiter.Name = request.Name;
        recruiter.LastName = request.LastName;
        recruiter.MiddleName = request.MiddleName;
        recruiter.Email = request.Email;
        recruiter.Phone = request.Phone;
        recruiter.WorkExperience = request.WorkExperience;
        foreach (var job in request.Jobs)
        {
            if (recruiter.Jobs.Any(j => j.Id == job.Id))
                continue;
            var jobToAdd = await _db.Jobs.FirstOrDefaultAsync(j => j.Id == job.Id, cancellationToken);
            if (jobToAdd is null)
            {
                var entityEntry = await _db.Jobs.AddAsync(job, cancellationToken);
                jobToAdd = entityEntry.Entity;
            }
            recruiter.Jobs.Add(jobToAdd);
        }

        foreach (var interview in request.Interviews)
        {
            if (recruiter.Interviews.Any(i => i.Id == interview.Id))
                continue;
            var interviewToAdd = await _db.Interviews.FirstOrDefaultAsync(i => i.Id == interview.Id, cancellationToken);
            if (interviewToAdd is null)
            {
                var entityEntry = await _db.Interviews.AddAsync(interview, cancellationToken);
                interviewToAdd = entityEntry.Entity;
            }

            recruiter.Interviews.Add(interviewToAdd);
        }

        await _db.SaveChangesAsync(cancellationToken);
        return recruiter;
    }
}
