namespace MilkaHR.Application.Candidate.Commands.GetAllCandidatesByStatusByJob;

public class Statistics
{
    public Statistics()
    {
        UnderInvestigation = 0;
        Accepted = 0;
        Rejected = 0;
    }

    public Statistics(int underInvestigation, int accepted, int rejected)
    {
        UnderInvestigation = underInvestigation;
        Accepted = accepted;
        Rejected = rejected;
    }
    
    public int UnderInvestigation { get; set; }
    public int Accepted { get; set; }
    public int Rejected { get; set; }
}
