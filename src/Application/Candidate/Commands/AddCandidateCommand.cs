using MilkaHR.Application.Common.Interfaces;
using MilkaHR.Domain.Enums;

namespace MilkaHR.Application.Candidate.Commands;

public record AddCandidateCommand(
    string Name,
    string LastName,
    string MiddleName,
    string Email,
    string Phone,
    string Address,
    DateTime BirthDate,
    int WorkExperience,
    string LastJob,
    string Education,
    string? Photo,
    List<Domain.Entities.Job> Jobs) : IRequest<MilkaHR.Domain.Entities.Candidate>;

public class AddCandidateCommandHandler(IApplicationDbContext db) : IRequestHandler<AddCandidateCommand, Domain.Entities.Candidate>
{
    public async Task<Domain.Entities.Candidate> Handle(AddCandidateCommand request, CancellationToken cancellationToken)
    {
        var candidate = new Domain.Entities.Candidate
        {
            Name = request.Name,
            MiddleName = request.MiddleName,
            LastName = request.LastName,
            Email = request.Email,
            Phone = request.Phone,
            Address = request.Address,
            BirthDate = request.BirthDate,
            WorkExperience = request.WorkExperience,
            LastJob = request.LastJob,
            Education = request.Education,
            Photo = request.Photo,
            JobStatuses = [],
            Cvs = [],
            Interviews = [],
            Tags = []
        };
        foreach (var job in request.Jobs)
        {
            var newCandidateJobProcessing = new Domain.Entities.CandidateJobProcessing
            {
                ProcessingStatus = CandidateStatus.CvCreated, Candidate = candidate, Job = job
            };
            await db.CandidateJobProcessings.AddAsync(newCandidateJobProcessing, cancellationToken);
            candidate.JobStatuses.Add(newCandidateJobProcessing);
        }
        var cvs = await db.Cvs.Where(x => x.Candidate.Id == candidate.Id).ToListAsync(cancellationToken);
        foreach (var cv in cvs)
        {
            candidate.Cvs.Add(cv);
        }
        await db.Candidates.AddAsync(candidate, cancellationToken);
        await db.SaveChangesAsync(cancellationToken);
        return candidate;
    }
}
