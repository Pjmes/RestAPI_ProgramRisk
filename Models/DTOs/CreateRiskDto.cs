using System.ComponentModel.DataAnnotations;
using ProgramRiskAPI.Models.Enums;

namespace ProgramRiskAPI.Models.DTOs;

public class CreateRiskDto
{
    [Required, MinLength(3)]
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    [Required]
    public string ProgramId { get; set; } = string.Empty;
    public string Owner { get; set; } = string.Empty;
    [Required]
    public RiskProbability Probability { get; set; }
    [Required]
    public RiskImpact Impact { get; set; }
}