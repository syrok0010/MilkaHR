using MilkaHR.Application.Common.Interfaces;
using MilkaHR.Domain.Entities;
using RecruiterEntity = MilkaHR.Domain.Entities.Recruiter;

namespace MilkaHR.Application.Recruiter.Commands;

public record CreateRecruiterCommand(string Name, string LastName, string MiddleName, string Email,
    string Phone, byte WorkExperience, List<Job> Jobs) : IRequest<RecruiterEntity>;

public class CreateRecruiterCommandHandler(IApplicationDbContext db)
    : IRequestHandler<CreateRecruiterCommand, RecruiterEntity>
{
    private readonly IApplicationDbContext _db = db;
    
    public async Task<RecruiterEntity> Handle(CreateRecruiterCommand request, CancellationToken cancellationToken)
    {
        var newRecruiter = new RecruiterEntity
        {
            Name = request.Name,
            LastName = request.LastName,
            MiddleName = request.MiddleName,
            Email = request.Email,
            Phone = request.Phone,
            WorkExperience = request.WorkExperience,
            Jobs = request.Jobs
        };
        await _db.Recruiters.AddAsync(newRecruiter, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);
        return newRecruiter;
    }
} 
