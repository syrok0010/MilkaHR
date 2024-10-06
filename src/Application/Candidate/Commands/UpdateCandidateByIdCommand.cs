using MilkaHR.Application.Common.Interfaces;
using MilkaHR.Domain.Entities;

namespace MilkaHR.Application.Candidate.Commands;

public record UpdateCandidateByIdCommand
(
    int Id,
    string Name,
    string LastName,
    string MiddleName,
    string Email,
    string Phone,
    string Address,
    int WorkExperience,
    string LastJob,
    string Education,
    string? Photo,
    List<Cv> Cvs,
    DateTime BirthDate) : IRequest;

public class UpdateCandidateByIdCommandHandler(IApplicationDbContext db) : IRequestHandler<UpdateCandidateByIdCommand>
{
    public async Task Handle(UpdateCandidateByIdCommand request, CancellationToken cancellationToken)
    {
        var candidate = await db.Candidates.FirstAsync(x => x.Id == request.Id, cancellationToken);
        candidate.Name = request.Name;
        candidate.MiddleName = request.MiddleName;
        candidate.LastName = request.LastName;
        candidate.Email = request.Email;
        candidate.Phone = request.Phone;
        candidate.Address = request.Address;
        candidate.WorkExperience = request.WorkExperience;
        candidate.LastJob = request.LastJob;
        candidate.Education = request.Education;
        candidate.Photo = request.Photo;
        candidate.BirthDate = request.BirthDate;
        foreach (var cv in request.Cvs)
        {
            if (cv.Candidate.Id == request.Id)
                candidate.Cvs.Add(cv);
        }
        await db.SaveChangesAsync(cancellationToken);
    }
}
