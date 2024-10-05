using MilkaHR.Application.Common.Interfaces;

namespace MilkaHR.Application.Recruiter.Commands;

public record DeleteRecruiterCommand(int Id) : IRequest<bool>;

public class DeleteRecruiterCommandHandler(IApplicationDbContext db)
    : IRequestHandler<DeleteRecruiterCommand, bool>
{
    private readonly IApplicationDbContext _db = db;

    public async Task<bool> Handle(DeleteRecruiterCommand request, CancellationToken cancellationToken)
    {
        var recruiter = await _db.Recruiters.FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);
        if (recruiter is null)
            return false;
        _db.Recruiters.Remove(recruiter);
        await _db.SaveChangesAsync(cancellationToken);
        return true;
    }
}
