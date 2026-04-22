using ProgramRiskAPI.Models.Enums;

namespace ProgramRiskAPI.Models;

public class Risk
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ProgramId { get; set; } = string.Empty;
    public string Owner { get; set; } = string.Empty;
    public RiskProbability Probability { get; set; }
    public RiskImpact Impact { get; set; }
    public int RiskScore => (int)Probability * (int)Impact;   // calculated, not stored
    public RiskStatus Status { get; set; } = RiskStatus.Identified;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; } = false;
}