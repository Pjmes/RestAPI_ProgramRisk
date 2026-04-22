using ProgramRiskAPI.Models;
using ProgramRiskAPI.Models.DTOs;
using ProgramRiskAPI.Models.Enums;

namespace ProgramRiskAPI.Services;

public class RiskService : IRiskService
{
    private readonly List<Risk> _risks = new();
    private int _nextId = 1;

    public IEnumerable<RiskResponseDto> GetAll() =>
        _risks.Where(r => !r.IsDeleted).Select(ToDto);

    public RiskResponseDto? GetById(int id) =>
        _risks.FirstOrDefault(r => r.Id == id && !r.IsDeleted) is { } risk ? ToDto(risk) : null;

    public RiskResponseDto Create(CreateRiskDto dto)
    {
        var risk = new Risk
        {
            Id = _nextId++,
            Title = dto.Title,
            Description = dto.Description,
            ProgramId = dto.ProgramId,
            Owner = dto.Owner,
            Probability = dto.Probability,
            Impact = dto.Impact
        };
        _risks.Add(risk);
        return ToDto(risk);
    }

    public RiskResponseDto? UpdateStatus(int id, RiskStatus newStatus)
    {
        var risk = _risks.FirstOrDefault(r => r.Id == id && !r.IsDeleted);
        if (risk is null) return null;
        risk.Status = newStatus;
        risk.UpdatedAt = DateTime.UtcNow;
        return ToDto(risk);
    }

    public bool Delete(int id)
    {
        var risk = _risks.FirstOrDefault(r => r.Id == id && !r.IsDeleted);
        if (risk is null) return false;
        risk.IsDeleted = true;
        risk.UpdatedAt = DateTime.UtcNow;
        return true;
    }

    private static RiskResponseDto ToDto(Risk r) => new()
    {
        Id = r.Id,
        Title = r.Title,
        Description = r.Description,
        ProgramId = r.ProgramId,
        Owner = r.Owner,
        Probability = r.Probability.ToString(),
        Impact = r.Impact.ToString(),
        RiskScore = r.RiskScore,
        Status = r.Status.ToString(),
        CreatedAt = r.CreatedAt,
        UpdatedAt = r.UpdatedAt
    };
}