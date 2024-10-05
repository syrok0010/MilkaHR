using MilkaHR.Application.Common.Interfaces;

namespace MilkaHR.Application.Job.Queries;

public record GetMonthlyAverageJobLifeTimeQuery : IRequest<Dictionary<int, double>>;

public class GetMonthlyAverageJobLifeTimeQueryHandler(IApplicationDbContext db) : IRequestHandler<GetMonthlyAverageJobLifeTimeQuery, Dictionary<int, double>>
{
    public Task<Dictionary<int, double>> Handle(GetMonthlyAverageJobLifeTimeQuery request, CancellationToken cancellationToken)
    {
        var currentMonth = DateTime.UtcNow;

        return db.Jobs
            .Where(x => x.ClosingDate.HasValue)
            .Where(x => x.ClosingDate > currentMonth.AddMonths(-6))
            .GroupBy(x => x.ClosingDate!.Value.Month)
            .Select(g => new
            {
                Month = g.Key,
                Average = g.Average(x => (x.ClosingDate!.Value - x.PublicationDate).TotalDays)
            })
            .ToDictionaryAsync(x => x.Month, x => x.Average, cancellationToken: cancellationToken);
    }
}

