using Lisport.API.Application.DTOs;
using Lisport.API.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Lisport.API.Controllers
{
    [ApiController]
    [Route("classes")]
    [Authorize(Roles = "Professor")]
    public class ClassesController : ControllerBase
    {
        private readonly IClassService _classService;
        private readonly IStudentService _studentService;

        public ClassesController(IClassService classService, IStudentService studentService)
        {
            _classService = classService;
            _studentService = studentService;
        }

        [HttpPost]
        public ActionResult Create([FromBody] CreateClassRequestDto request)
        {
            if (!TryGetCurrentUserId(out var professorId))
            {
                return Unauthorized();
            }

            var classGroup = _classService.Create(request.Name, request.Modality, request.AgeRange, request.DaysAndTimes, professorId);
            var response = MapClass(classGroup);
            return Created("", response);
        }

        [HttpGet]
        public ActionResult<IEnumerable<ClassResponseDto>> GetAll()
        {
            if (!TryGetCurrentUserId(out var professorId))
            {
                return Unauthorized();
            }

            var classes = _classService.GetByProfessor(professorId)
                .Select(MapClass)
                .ToList();

            return Ok(classes);
        }

        [HttpGet("{id:guid}")]
        public ActionResult<ClassResponseDto> GetById(Guid id)
        {
            if (!TryGetCurrentUserId(out var professorId))
            {
                return Unauthorized();
            }

            var classGroup = _classService.GetById(id, professorId);
            if (classGroup == null)
            {
                return NotFound();
            }

            return Ok(MapClass(classGroup));
        }

        [HttpPatch("{id:guid}")]
        public ActionResult<ClassResponseDto> Update(Guid id, [FromBody] UpdateClassRequestDto request)
        {
            if (!TryGetCurrentUserId(out var professorId))
            {
                return Unauthorized();
            }

            var classGroup = _classService.Update(id, professorId, request.Name, request.Modality, request.AgeRange, request.DaysAndTimes);
            if (classGroup == null)
            {
                return NotFound();
            }

            return Ok(MapClass(classGroup));
        }

        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            if (!TryGetCurrentUserId(out var professorId))
            {
                return Unauthorized();
            }

            var deleted = _classService.Delete(id, professorId);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet("{classId:guid}/students")]
        public ActionResult<IEnumerable<StudentResponseDto>> GetStudents(Guid classId)
        {
            if (!TryGetCurrentUserId(out var professorId))
            {
                return Unauthorized();
            }

            var students = _studentService.GetByClass(classId, professorId)
                .Select(MapStudent)
                .ToList();

            return Ok(students);
        }

        private static ClassResponseDto MapClass(Domain.Entities.ClassGroup classGroup)
        {
            return new ClassResponseDto
            {
                Id = classGroup.Id,
                Name = classGroup.Name,
                Modality = classGroup.Modality,
                AgeRange = classGroup.AgeRange,
                DaysAndTimes = classGroup.DaysAndTimes,
                ProfessorId = classGroup.ProfessorId,
                CreatedAt = classGroup.CreatedAt
            };
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
