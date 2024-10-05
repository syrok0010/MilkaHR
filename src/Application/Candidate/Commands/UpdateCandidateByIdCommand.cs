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
    int SalaryPreference,
    List<Cv> Cvs) : IRequest;

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
        candidate.SalaryPreference = request.SalaryPreference;
        foreach (var cv in request.Cvs)
        {
            if (cv.Candidate.Id == request.Id)
                candidate.Cvs.Add(cv);
        }
        await db.SaveChangesAsync(cancellationToken);
    }
}
