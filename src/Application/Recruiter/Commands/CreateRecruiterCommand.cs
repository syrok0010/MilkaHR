using MilkaHR.Application.Common.Interfaces;

namespace MilkaHR.Application.Recruiter.Commands;

public record CreateRecruiterCommand(string Name, string LastName, string MiddleName, string Email,
    string Phone, byte WorkExperience, List<Domain.Entities.Job> Jobs) : IRequest<Domain.Entities.Recruiter>;

public class CreateRecruiterCommandHandler(IApplicationDbContext db)
    : IRequestHandler<CreateRecruiterCommand, Domain.Entities.Recruiter>
{
    private readonly IApplicationDbContext _db = db;
    
    public async Task<Domain.Entities.Recruiter> Handle(CreateRecruiterCommand request, CancellationToken cancellationToken)
    {
        var newRecruiter = new Domain.Entities.Recruiter
        {
            Name = request.Name,
            LastName = request.LastName,
            MiddleName = request.MiddleName,
            Email = request.Email,
            Phone = request.Phone,
            WorkExperience = request.WorkExperience,
            Jobs = [],
            Interviews = []
        };
        foreach (var job in request.Jobs)
        {
            var jobToAdd = await _db.Jobs.FirstOrDefaultAsync(j => j.Id == job.Id, cancellationToken);
            if (jobToAdd is null)
            {
                var entityEntry = await _db.Jobs.AddAsync(job, cancellationToken);
                jobToAdd = entityEntry.Entity;
            }
            newRecruiter.Jobs.Add(jobToAdd);
        }
        await _db.Recruiters.AddAsync(newRecruiter, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);
        return newRecruiter;
    }
} 
