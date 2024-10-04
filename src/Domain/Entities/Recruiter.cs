﻿namespace MilkaHR.Domain.Entities;

public class Recruiter : BaseAuditableEntity
{
    public required string Name { get; set; }
    public required string LastName { get; set; }
    public required string MiddleName { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }
    public required byte WorkExperience { get; set; }
    public required List<Job> Jobs { get; set; } = new();
}
