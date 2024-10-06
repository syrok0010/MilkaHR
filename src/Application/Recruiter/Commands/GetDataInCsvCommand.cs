using System.IO.Compression;
using MilkaHR.Application.Candidate.Queries;
using MilkaHR.Application.Common.Interfaces;
using MilkaHR.Application.Job.Queries;
using MilkaHR.Domain.Enums;

namespace MilkaHR.Application.Recruiter.Commands;

public record GetDataInCsvCommand : IRequest<string>;

public class GetDataInCsvCommandHandler(IApplicationDbContext db) : IRequestHandler<GetDataInCsvCommand, string>
{
    private readonly IApplicationDbContext _db = db;

    public async Task<string> Handle(GetDataInCsvCommand request, CancellationToken cancellationToken)
    {
        var jobs = await _db.Jobs.Where(j => j.Status == JobStatus.Opened).ToListAsync(cancellationToken);
        var numberOfCandidatesInEachOpenedJob = new Dictionary<string, List<int>>();
        foreach (var job in jobs)
        {
            numberOfCandidatesInEachOpenedJob.Add(job.Title, []);
            var processingStatuses = await _db.CandidateJobProcessings
                .Where(x => x.Job.Id == job.Id)
                .GroupBy(x => x.ProcessingStatus)
                .Select(g => new { ProcessingStatus = g.Key, Count = g.Count() })
                .ToListAsync(cancellationToken);
            if (processingStatuses.Count == 0)
                continue;
            foreach (var status in processingStatuses.OrderBy(x => x.ProcessingStatus))
            {
                numberOfCandidatesInEachOpenedJob[job.Title].Add(status.Count);
            }
        }
        var resultByStatusByJob = numberOfCandidatesInEachOpenedJob.Count == 0 ? null : numberOfCandidatesInEachOpenedJob;
        var dataByStatusByJob = Utilities.GetCsvByStatusByJob(resultByStatusByJob!);

        var counts = new List<int>();
        foreach (var job in jobs)
        {
            var count = await _db.Candidates.CountAsync(x => x.JobStatuses.Any(y => y.Job.Id == job.Id), cancellationToken);
            counts.Add(count);
        }

        var dataCandidatesCountByJob = Utilities.GetCsvCandidatesCountByJob(counts);

        var resultJobsCountByPriority =
            await _db.Jobs
                .GroupBy(
                    x => x.Priority,
                    x => x,
                    (level, jobs3) => new StatisticByPriority(
                        level, 
                        jobs3.Count(x => x.Status == JobStatus.Opened), 
                        jobs3.Count()
                    )
                ).ToListAsync(cancellationToken);
        var dataJobsCountByPriority = Utilities.GetCsvJobsCountByPriority(resultJobsCountByPriority);

        File.Delete("result.zip");
        Directory.Delete("results", true);
        var directoryInfo = Directory.CreateDirectory("results");
        await File.WriteAllTextAsync("results/1.csv", dataByStatusByJob, cancellationToken);
        await File.WriteAllTextAsync("results/2.csv", dataCandidatesCountByJob, cancellationToken);
        await File.WriteAllTextAsync("results/3.csv", dataJobsCountByPriority, cancellationToken);
        
        ZipFile.CreateFromDirectory("results", "result.zip");
        var file = new FileInfo("result.zip");
        var path = file.FullName;


        return path;
    }
}
