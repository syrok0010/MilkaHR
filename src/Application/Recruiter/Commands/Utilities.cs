using System.Text;
using Microsoft.Extensions.Primitives;
using MilkaHR.Application.Job.Queries;

namespace MilkaHR.Application.Recruiter.Commands;

public static class Utilities
{
    public static string GetCsvByStatusByJob(Dictionary<string, List<int>> data)
    {
        var builder = new StringBuilder();
        builder.Append("Вакансия,Резюме одобрено,Собеседование назначено,Собеседование проведено,Нанят,Отклонен\n");
        foreach (var pair in data)
        {
            builder.Append($"{pair.Key} {string.Join(",", pair.Value.Select(x => x.ToString().ToArray()))}").Append('\n');
        }

        return builder.ToString();
    }

    public static string GetCsvCandidatesCountByJob(List<int> data)
    {
        return string.Join(",", data);
    }

    public static string GetCsvJobsCountByPriority(List<StatisticByPriority> data)
    {
        var builder = new StringBuilder();
        builder.Append("Приоритет,Закрытые заявки,Все заявки\n");
        var orderedData = data.OrderBy(x => x.Level).ToList();
        foreach (var statistic in data)
        {
            builder.Append(
                    statistic.Level.ToString())
                .Append(',')
                .Append(statistic.Opened)
                .Append(',')
                .Append(statistic.All)
                .Append('\n');
        }

        return builder.ToString();
    }
}
