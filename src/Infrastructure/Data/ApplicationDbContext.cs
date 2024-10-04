using System.Reflection;
using MilkaHR.Application.Common.Interfaces;
using MilkaHR.Domain.Entities;
using MilkaHR.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MilkaHR.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Candidate> Candidates => Set<Candidate>();
    public DbSet<Cv> Cvs => Set<Cv>();
    public DbSet<Job> Jobs => Set<Job>();
    public DbSet<Recruiter> Recruiters => Set<Recruiter>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
