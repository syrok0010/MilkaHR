using MilkaHR.Domain.Enums;

namespace MilkaHR.Application.Job.Queries;

public record StatisticByPriority(PriorityLevel Level, int Opened, int All);
