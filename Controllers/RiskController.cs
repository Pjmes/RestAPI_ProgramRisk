using Microsoft.AspNetCore.Mvc;
using ProgramRiskAPI.Models.DTOs;
using ProgramRiskAPI.Services;

namespace ProgramRiskAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RisksController : ControllerBase
{
    private readonly IRiskService _service;
    public RisksController(IRiskService service) => _service = service;

    [HttpGet]
    public IActionResult GetAll() => Ok(_service.GetAll());

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var risk = _service.GetById(id);
        return risk is null ? NotFound() : Ok(risk);
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateRiskDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var created = _service.Create(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPatch("{id}/status")]
    public IActionResult UpdateStatus(int id, [FromBody] UpdateStatusDto dto)
    {
        var updated = _service.UpdateStatus(id, dto.NewStatus);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        return _service.Delete(id) ? NoContent() : NotFound();
    }
}