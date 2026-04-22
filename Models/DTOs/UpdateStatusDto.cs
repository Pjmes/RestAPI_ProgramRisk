using System.ComponentModel.DataAnnotations;
using ProgramRiskAPI.Models.Enums;

namespace ProgramRiskAPI.Models.DTOs;

public class UpdateStatusDto
{
    [Required]
    public RiskStatus NewStatus { get; set; }
}