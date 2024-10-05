using MilkaHR.Application.Common.Interfaces;

namespace MilkaHR.Application.Note.Queries;

public record GetAllNotesQuery: IRequest<IEnumerable<Domain.Entities.Note>>;

public class GetAllNotesQueryHandler(IApplicationDbContext db)
    : IRequestHandler<GetAllNotesQuery, IEnumerable<Domain.Entities.Note>>
{
    private readonly IApplicationDbContext _db = db;
    
    public Task<IEnumerable<Domain.Entities.Note>> Handle(GetAllNotesQuery request, 
        CancellationToken cancellationToken)
    {
        return Task.FromResult<IEnumerable<Domain.Entities.Note>>(_db.Notes);
    }
}
