using Lisport.API.Application.DTOs;
using Lisport.API.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Lisport.API.Controllers
{
    [ApiController]
    [Route("students")]
    [Authorize(Roles = "Professor")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpPost]
        public ActionResult Create([FromBody] CreateStudentRequestDto request)
        {
            if (!TryGetCurrentUserId(out var professorId))
            {
                return Unauthorized();
            }

            try
            {
                var student = _studentService.Create(
                    request.Name,
                    request.BirthDate,
                    request.Gender,
                    request.Document,
                    request.ResponsibleName,
                    request.Phone,
                    request.Address,
                    request.Notes,
                    request.ClassGroupId,
                    professorId);

                return Created("", MapStudent(student));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id:guid}")]
        public ActionResult<StudentResponseDto> GetById(Guid id)
        {
            if (!TryGetCurrentUserId(out var professorId))
            {
                return Unauthorized();
            }

            var student = _studentService.GetById(id, professorId);
            if (student == null)
            {
                return NotFound();
            }

            return Ok(MapStudent(student));
        }

        [HttpPatch("{id:guid}")]
        public ActionResult<StudentResponseDto> Update(Guid id, [FromBody] UpdateStudentRequestDto request)
        {
            if (!TryGetCurrentUserId(out var professorId))
            {
                return Unauthorized();
            }

            var student = _studentService.Update(
                id,
                professorId,
                request.Name,
                request.BirthDate,
                request.Gender,
                request.Document,
                request.ResponsibleName,
                request.Phone,
                request.Address,
                request.Notes);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(MapStudent(student));
        }

        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            if (!TryGetCurrentUserId(out var professorId))
            {
                return Unauthorized();
            }

            var deleted = _studentService.Delete(id, professorId);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        private static StudentResponseDto MapStudent(Domain.Entities.Student student)
        {
            return new StudentResponseDto
            {
                Id = student.Id,
                Name = student.Name,
                BirthDate = student.BirthDate,
                Gender = student.Gender,
                Document = student.Document,
                ResponsibleName = student.ResponsibleName,
                Phone = student.Phone,
                Address = student.Address,
                Notes = student.Notes,
                ClassGroupId = student.ClassGroupId,
                CreatedAt = student.CreatedAt
            };
        }

        private bool TryGetCurrentUserId(out Guid userId)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Guid.TryParse(userIdClaim, out userId);
        }
    }
}
