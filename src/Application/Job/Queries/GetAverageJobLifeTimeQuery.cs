using MilkaHR.Application.Common.Interfaces;
using MilkaHR.Domain.Enums;

namespace MilkaHR.Application.Job.Queries;

public record GetAverageJobLifeTimeQuery(int Months) : IRequest<Dictionary<string, double>>;

public class GetAverageJobLifeTimeQueryHandler(IApplicationDbContext db) 
    : IRequestHandler<GetAverageJobLifeTimeQuery, Dictionary<string, double>>
{
    public async Task<Dictionary<string, double>> Handle(GetAverageJobLifeTimeQuery request,
        CancellationToken cancellationToken)
    {
        return await db.Jobs
            .Where(x => x.ClosingDate.HasValue &&
                        x.PublicationDate >= DateTime.UtcNow.AddMonths(-request.Months))
            .GroupBy(x => x.Category)
            .Select(x => new
            {
                Category = x.Key, Time = x.Average(j => (j.ClosingDate!.Value - j.PublicationDate).TotalDays)
            })
            .OrderBy(x => x.Category)
            .ToDictionaryAsync(x => x.Category.ToString(), x => x.Time, cancellationToken);
    }
}

