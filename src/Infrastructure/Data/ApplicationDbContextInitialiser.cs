using MilkaHR.Domain.Constants;
using MilkaHR.Domain.Entities;
using MilkaHR.Infrastructure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MilkaHR.Domain.Enums;

namespace MilkaHR.Infrastructure.Data;

public static class InitialiserExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

        await initialiser.InitialiseAsync();

        await initialiser.SeedAsync();
    }
}

public class ApplicationDbContextInitialiser(
    ILogger<ApplicationDbContextInitialiser> logger,
    ApplicationDbContext context,
    UserManager<ApplicationUser> userManager,
    RoleManager<IdentityRole> roleManager)
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger = logger;
    private readonly ApplicationDbContext _context = context;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly RoleManager<IdentityRole> _roleManager = roleManager;

    public async Task InitialiseAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default roles
        var administratorRole = new IdentityRole(Roles.Administrator);

        if (_roleManager.Roles.All(r => r.Name != administratorRole.Name))
        {
            await _roleManager.CreateAsync(administratorRole);
        }

        // Default users
        var administrator = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost" };

        if (_userManager.Users.All(u => u.UserName != administrator.UserName))
        {
            await _userManager.CreateAsync(administrator, "Administrator1!");
            if (!string.IsNullOrWhiteSpace(administratorRole.Name))
            {
                await _userManager.AddToRolesAsync(administrator, [administratorRole.Name]);
            }
        }

        if (await _context.Jobs.AnyAsync())
            return;

        var recruiter = await _context.AddAsync(new Recruiter
        {
            Name = "Иван",
            LastName = "Иванов",
            MiddleName = "Иванович",
            Email = "administrator@localhost.ru",
            Phone = "81234567890",
            WorkExperience = 5,
            Jobs = [],
            Interviews = [],
        });

        var jobs = new List<Job>
        {
            new()
            {
                Title = "Разработчик",
                Priority = PriorityLevel.Medium,
                Status = JobStatus.Opened,
                PublicationDate = DateTime.UtcNow,
                Recruiter = recruiter.Entity,
                CandidateStatuses = [],
                Category = 0
            },
            new Job
            {
                Title = "Тестировщик",
                Priority = PriorityLevel.VeryLow,
                Status = JobStatus.Opened,
                PublicationDate = DateTime.UtcNow,
                Recruiter = recruiter.Entity,
                CandidateStatuses = [],
                Category = 0
            },
            new Job
            {
                Title = "Кофе-носитель",
                Priority = PriorityLevel.VeryHigh,
                Status = JobStatus.Opened,
                PublicationDate = DateTime.UtcNow,
                Recruiter = recruiter.Entity,
                CandidateStatuses = [],
                Category = 0
            },
            new Job
            {
                Title = "Тимлид",
                Priority = PriorityLevel.High,
                Status = JobStatus.Opened,
                PublicationDate = DateTime.UtcNow,
                Recruiter = recruiter.Entity,
                CandidateStatuses = [],
                Category = 0
            },
            new Job
            {
                Title = "Аналитик",
                Priority = PriorityLevel.Low,
                Status = JobStatus.Opened,
                PublicationDate = DateTime.UtcNow,
                Recruiter = recruiter.Entity,
                CandidateStatuses = [],
                Category = 0
            },
            new Job
            {
                Title = "UI-дизайнер",
                Priority = PriorityLevel.High,
                Status = JobStatus.Opened,
                PublicationDate = DateTime.UtcNow,
                Recruiter = recruiter.Entity,
                CandidateStatuses = [],
                Category = 0
            },
            new Job
            {
                Title = "DevOps",
                Priority = PriorityLevel.Medium,
                Status = JobStatus.Opened,
                PublicationDate = DateTime.UtcNow,
                Recruiter = recruiter.Entity,
                CandidateStatuses = [],
                Category = 0
            },
            new Job
            {
                Title = "Техлид",
                Priority = PriorityLevel.Medium,
                Status = JobStatus.Opened,
                PublicationDate = DateTime.UtcNow,
                Recruiter = recruiter.Entity,
                CandidateStatuses = [],
                Category = 0
            },
            new Job
            {
                Title = "UX-дизайнер",
                Priority = PriorityLevel.VeryLow,
                Status = JobStatus.Opened,
                PublicationDate = DateTime.UtcNow,
                Recruiter = recruiter.Entity,
                CandidateStatuses = [],
                Category = 0
            }
        };
        await _context.AddRangeAsync(jobs);

        var c1 = new Candidate
        {
            Name = "Сергей",
            MiddleName = "Иванович",
            LastName = "Петров",
            Address = "Улица Минина д.13",
            BirthDate = DateTime.UtcNow.AddYears(-20).AddMonths(-3).AddDays(-20),
            Education = "Высшее",
            Email = "zanoza@mail.ru",
            Phone = "89913342873",
            WorkExperience = 5,
            LastJob = "Tinkoff",
            Cvs = [],
            Tags = ["OOP", "C++", "DataBase"],
            JobStatuses = [],
            Photo = "data:image/png;base64,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"
        };
        c1.JobStatuses.Add(new CandidateJobProcessing
        {
            Candidate = c1, Job = jobs[0], ProcessingStatus = CandidateStatus.InterviewScheduled
        });

        var birthDate2 = DateTime.Parse("1967.08.12");
        DateTime.SpecifyKind(birthDate2, DateTimeKind.Utc);
        var c2 = new Candidate
        {
            Name = "Виталий",
            MiddleName = "Ибрагимович",
            LastName = "Солдатов",
            Address = "Проспект Серьезного д. 12/3",
            BirthDate = DateTime.UtcNow.AddYears(-30).AddMonths(-8).AddDays(-12),
            Education = "Высшее",
            Email = "megasoldat@gmail.com",
            Phone = "89126691002",
            WorkExperience = 23,
            LastJob = "STM-labs",
            Cvs = [],
            Tags = ["Go", "Angular", "DevOps"],
            JobStatuses = [],
            Photo = "data:image/png;base64,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"
        };
        c2.JobStatuses.Add(new CandidateJobProcessing
        {
            Candidate = c2, Job = jobs[2], ProcessingStatus = CandidateStatus.CvApproved
        });
        c2.JobStatuses.Add(new CandidateJobProcessing
        {
            Candidate = c2, Job = jobs[3], ProcessingStatus = CandidateStatus.InterviewScheduled
        });
        c2.JobStatuses.Add(new CandidateJobProcessing
        {
            Candidate = c2, Job = jobs[4], ProcessingStatus = CandidateStatus.InterviewScheduled
        });

        var birthDate3 = DateTime.Parse("2002.11.02");
        DateTime.SpecifyKind(birthDate3, DateTimeKind.Utc);
        var c3 = new Candidate
        {
            Name = "Анатолий",
            MiddleName = "Маратович",
            LastName = "Зайцев",
            Address = "Бульвар мира д. 1",
            BirthDate = DateTime.UtcNow.AddYears(-40).AddMonths(-5).AddDays(-29),
            Education = "Высшее",
            Email = "tolyanzzzaaay@gmail.com",
            Phone = "89192394744",
            WorkExperience = 8,
            LastJob = "Yandex",
            Cvs = [],
            Tags = ["UX/UI", "Web", "Java"],
            JobStatuses = [],
            Photo = "data:image/png;base64,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"
        };
        c3.JobStatuses.Add(new CandidateJobProcessing()
        {
            Candidate = c3, Job = jobs[5], ProcessingStatus = CandidateStatus.InterviewCompleted
        });
        c3.JobStatuses.Add(new CandidateJobProcessing()
        {
            Candidate = c3, Job = jobs[6], ProcessingStatus = CandidateStatus.InterviewScheduled
        });
        c3.JobStatuses.Add(new CandidateJobProcessing()
        {
            Candidate = c3, Job = jobs[7], ProcessingStatus = CandidateStatus.InterviewScheduled
        });
        c3.JobStatuses.Add(new CandidateJobProcessing()
        {
            Candidate = c3, Job = jobs[8], ProcessingStatus = CandidateStatus.InterviewScheduled
        });

        await _context.AddRangeAsync(c1, c2, c3);

        var interview1 = new Interview
        {
            Candidate = c1, Job = jobs[0], Timing = DateTime.UtcNow.AddDays(10), Type = EventType.VideoConference
        };

        var interview2 = new Interview
        {
            Candidate = c2, Job = jobs[3], Timing = DateTime.UtcNow.AddDays(11), Type = EventType.Meeting
        };
        
        var interview3 = new Interview
        {
            Candidate = c2, Job = jobs[4], Timing = DateTime.UtcNow.AddDays(12), Type = EventType.Interview
        };

        var interview4 = new Interview
        {
            Candidate = c3, Job = jobs[6], Timing = DateTime.UtcNow.AddDays(13), Type = EventType.Ride
        };
        
        var interview5 = new Interview
        {
            Candidate = c3, Job = jobs[7], Timing = DateTime.UtcNow.AddDays(14), Type = EventType.Meeting
        };

        var interview6 = new Interview
        {
            Candidate = c3, Job = jobs[8], Timing = DateTime.UtcNow.AddDays(20), Type = EventType.Ride
        };

        recruiter.Entity.Interviews.Add(interview1);
        recruiter.Entity.Interviews.Add(interview2);
        recruiter.Entity.Interviews.Add(interview3);
        recruiter.Entity.Interviews.Add(interview4);
        recruiter.Entity.Interviews.Add(interview5);
        recruiter.Entity.Interviews.Add(interview6);
        
        
        await _context.AddRangeAsync(interview1, interview2, interview3, interview4, interview5, interview6);
        
        await _context.SaveChangesAsync();
    }
}
