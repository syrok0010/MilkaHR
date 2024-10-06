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
            Interviews = []
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
            BirthDate = DateTime.Parse("1999.12.03"),
            Education = "Высшее",
            Email = "zanoza@mail.ru",
            Phone = "89913342873",
            WorkExperience = 5,
            LastJob = "Tinkoff",
            Cvs = [],
            Tags = ["OOP", "C++", "DataBase"],
            JobStatuses = []
        };
        c1.JobStatuses.Add(new CandidateJobProcessing
        {
            Candidate = c1, Job = jobs[0], ProcessingStatus = CandidateStatus.InterviewScheduled
        });

        var c2 = new Candidate
        {
            Name = "Виталий",
            MiddleName = "Ибрагимович",
            LastName = "Солдатов",
            Address = "Проспект Серьезного д. 12/3",
            BirthDate = DateTime.Parse("1967.08.12"),
            Education = "Высшее",
            Email = "megasoldat@gmail.com",
            Phone = "89126691002",
            WorkExperience = 23,
            LastJob = "STM-labs",
            Cvs = [],
            Tags = ["Go", "Angular", "DevOps"],
            JobStatuses = []
        };
        c2.JobStatuses.Add(new CandidateJobProcessing
        {
            Candidate = c2, Job = jobs[2], ProcessingStatus = CandidateStatus.CvApproved
        });

        var c3 = new Candidate
        {
            Name = "Анатолий",
            MiddleName = "Маратович",
            LastName = "Зайцев",
            Address = "Бульвар мира д. 1",
            BirthDate = DateTime.Parse("2002.11.02"),
            Education = "Высшее",
            Email = "tolyanzzzaaay@gmail.com",
            Phone = "89192394744",
            WorkExperience = 8,
            LastJob = "Yandex",
            Cvs = [],
            Tags = ["UX/UI", "Web", "Java"],
            JobStatuses = [],
        };
        c3.JobStatuses.Add(new CandidateJobProcessing()
        {
            Candidate = c3, Job = jobs[5], ProcessingStatus = CandidateStatus.InterviewCompleted
        });

        await _context.AddRangeAsync(c1, c2, c3);
        
        await _context.SaveChangesAsync();
    }
}
