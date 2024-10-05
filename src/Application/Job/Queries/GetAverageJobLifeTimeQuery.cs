using MilkaHR.Application.Common.Interfaces;

namespace MilkaHR.Application.Job.Queries;

public record GetAverageJobLifeTimeQuery : IRequest<double>;

public class GetAverageJobLifeTimeQueryHandler(IApplicationDbContext db) : IRequestHandler<GetAverageJobLifeTimeQuery, double>
{
    public Task<double> Handle(GetAverageJobLifeTimeQuery request, CancellationToken cancellationToken) => 
        db.Jobs
            .Where(x => x.ClosingDate.HasValue && x.ClosingDate > DateTime.UtcNow.AddMonths(-6))
            .AverageAsync(x => (x.ClosingDate!.Value - x.PublicationDate).TotalDays, cancellationToken);
}

