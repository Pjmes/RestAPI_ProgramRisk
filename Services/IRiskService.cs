using ProgramRiskAPI.Models;
using ProgramRiskAPI.Models.DTOs;
using ProgramRiskAPI.Models.Enums;

namespace ProgramRiskAPI.Services;

public interface IRiskService
{
    IEnumerable<RiskResponseDto> GetAll();
    RiskResponseDto? GetById(int id);
    RiskResponseDto Create(CreateRiskDto dto);
    RiskResponseDto? UpdateStatus(int id, RiskStatus newStatus);
    bool Delete(int id);
}