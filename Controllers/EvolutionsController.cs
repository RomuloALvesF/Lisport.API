using Lisport.API.Application.DTOs;
using Lisport.API.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Lisport.API.Controllers
{
    [ApiController]
    [Route("evolutions")]
    [Authorize(Roles = "Professor")]
    public class EvolutionsController : ControllerBase
    {
        private readonly IEvolutionService _evolutionService;

        public EvolutionsController(IEvolutionService evolutionService)
        {
            _evolutionService = evolutionService;
        }

        [HttpPost]
        public ActionResult<StudentEvolutionResponseDto> Create([FromBody] CreateStudentEvolutionRequestDto request)
        {
            if (!TryGetCurrentUserId(out var professorId))
            {
                return Unauthorized();
            }

            try
            {
                var evolution = _evolutionService.Create(
                    request.StudentId,
                    request.Date,
                    request.PhysicalScore,
                    request.TechnicalScore,
                    request.BehaviorScore,
                    request.Notes,
                    professorId);

                return Created("", MapEvolution(evolution));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id:guid}")]
        public ActionResult<StudentEvolutionResponseDto> GetById(Guid id)
        {
            if (!TryGetCurrentUserId(out var professorId))
            {
                return Unauthorized();
            }

            var evolution = _evolutionService.GetById(id, professorId);
            if (evolution == null)
            {
                return NotFound();
            }

            return Ok(MapEvolution(evolution));
        }

        [HttpGet("students/{studentId:guid}")]
        public ActionResult<IEnumerable<StudentEvolutionResponseDto>> GetByStudent(Guid studentId)
        {
            if (!TryGetCurrentUserId(out var professorId))
            {
                return Unauthorized();
            }

            var evolutions = _evolutionService.GetByStudent(studentId, professorId)
                .Select(MapEvolution)
                .ToList();

            return Ok(evolutions);
        }

        [HttpPatch("{id:guid}")]
        public ActionResult<StudentEvolutionResponseDto> Update(Guid id, [FromBody] UpdateStudentEvolutionRequestDto request)
        {
            if (!TryGetCurrentUserId(out var professorId))
            {
                return Unauthorized();
            }

            try
            {
                var evolution = _evolutionService.Update(
                    id,
                    professorId,
                    request.PhysicalScore,
                    request.TechnicalScore,
                    request.BehaviorScore,
                    request.Notes);

                if (evolution == null)
                {
                    return NotFound();
                }

                return Ok(MapEvolution(evolution));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            if (!TryGetCurrentUserId(out var professorId))
            {
                return Unauthorized();
            }

            var deleted = _evolutionService.Delete(id, professorId);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        private static StudentEvolutionResponseDto MapEvolution(Domain.Entities.StudentEvolution evolution)
        {
            return new StudentEvolutionResponseDto
            {
                Id = evolution.Id,
                StudentId = evolution.StudentId,
                Date = evolution.Date,
                PhysicalScore = evolution.PhysicalScore,
                TechnicalScore = evolution.TechnicalScore,
                BehaviorScore = evolution.BehaviorScore,
                Notes = evolution.Notes,
                CreatedAt = evolution.CreatedAt,
                UpdatedAt = evolution.UpdatedAt
            };
        }

        private bool TryGetCurrentUserId(out Guid userId)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Guid.TryParse(userIdClaim, out userId);
        }
    }
}
