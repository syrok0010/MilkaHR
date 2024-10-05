using MilkaHR.Application.Common.Interfaces;

namespace MilkaHR.Application.Recruiter.Queries;

public record GetAllRecruitersQuery : IRequest<IEnumerable<Domain.Entities.Recruiter>>;

public class GetAllRecruitersQueryHandler(IApplicationDbContext db)
    : IRequestHandler<GetAllRecruitersQuery, IEnumerable<Domain.Entities.Recruiter>>
{
    private readonly IApplicationDbContext _db = db;
    
    public Task<IEnumerable<Domain.Entities.Recruiter>> Handle(GetAllRecruitersQuery request, 
        CancellationToken cancellationToken)
    {
        return Task.FromResult<IEnumerable<Domain.Entities.Recruiter>>(_db.Recruiters
            .Include(x => x.Jobs));
    }
}
