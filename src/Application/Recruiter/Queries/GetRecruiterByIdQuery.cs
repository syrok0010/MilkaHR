using MilkaHR.Application.Common.Interfaces;

namespace MilkaHR.Application.Recruiter.Queries;

public record GetRecruiterByIdQuery(int Id) : IRequest<Domain.Entities.Recruiter?>;

public class GetRecruiterByIdQueryHandler(IApplicationDbContext db)
    : IRequestHandler<GetRecruiterByIdQuery, Domain.Entities.Recruiter?>
{
    private readonly IApplicationDbContext _db = db;
    
    public Task<Domain.Entities.Recruiter?> Handle(GetRecruiterByIdQuery request, 
        CancellationToken cancellationToken)
    {
        return _db.Recruiters
            .Include(x => x.Jobs)
            .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);
    }
}
