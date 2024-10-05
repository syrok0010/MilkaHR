using MilkaHR.Domain.Enums;

namespace MilkaHR.Application.Job.Queries;

public class StatisticByPriority(PriorityLevel level, int closed, int all)
{
    public PriorityLevel Level { get; set; } = level;
    public int Closed { get; set; } = closed;
    public int All { get; set; } = all;
}
