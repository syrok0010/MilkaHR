using MilkaHR.Application.Common.Interfaces;
using MilkaHR.Domain.Enums;

namespace MilkaHR.Application.Candidate.Commands.UpdateCandidateById;

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
    CandidateStatus Status
) : IRequest;

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
        candidate.Status = request.Status;
        await db.SaveChangesAsync(cancellationToken);
    }
}
