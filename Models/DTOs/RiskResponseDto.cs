using ProgramRiskAPI.Models.Enums;

namespace ProgramRiskAPI.Models.DTOs;

public class RiskResponseDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ProgramId { get; set; } = string.Empty;
    public string Owner { get; set; } = string.Empty;
    public string Probability { get; set; } = string.Empty;
    public string Impact { get; set; } = string.Empty;
    public int RiskScore { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}