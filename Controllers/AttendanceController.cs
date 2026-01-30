using Lisport.API.Application.DTOs;
using Lisport.API.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Lisport.API.Controllers
{
    [ApiController]
    [Route("attendance")]
    [Authorize(Roles = "Professor")]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;

        public AttendanceController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        [HttpPost("sessions")]
        public ActionResult<AttendanceSessionResponseDto> CreateSession([FromBody] CreateAttendanceSessionRequestDto request)
        {
            if (!TryGetCurrentUserId(out var professorId))
            {
                return Unauthorized();
            }

            try
            {
                var session = _attendanceService.CreateSession(request.ClassGroupId, request.Date, professorId);
                return Ok(MapSession(session));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("sessions/{id:guid}")]
        public ActionResult<AttendanceSessionResponseDto> GetSession(Guid id)
        {
            if (!TryGetCurrentUserId(out var professorId))
            {
                return Unauthorized();
            }

            var session = _attendanceService.GetSession(id, professorId);
            if (session == null)
            {
                return NotFound();
            }

            return Ok(MapSession(session));
        }

        [HttpGet("classes/{classId:guid}/sessions")]
        public ActionResult<IEnumerable<AttendanceSessionResponseDto>> GetSessionsByClass(Guid classId)
        {
            if (!TryGetCurrentUserId(out var professorId))
            {
                return Unauthorized();
            }

            var sessions = _attendanceService.GetSessionsByClass(classId, professorId)
                .Select(MapSession)
                .ToList();

            return Ok(sessions);
        }

        [HttpGet("sessions/{id:guid}/records")]
        public ActionResult<IEnumerable<AttendanceRecordResponseDto>> GetRecords(Guid id)
        {
            if (!TryGetCurrentUserId(out var professorId))
            {
                return Unauthorized();
            }

            var records = _attendanceService.GetRecords(id, professorId)
                .Select(MapRecord)
                .ToList();

            return Ok(records);
        }

        [HttpPost("sessions/{id:guid}/mark")]
        public ActionResult<AttendanceRecordResponseDto> MarkAttendance(Guid id, [FromBody] MarkAttendanceRequestDto request)
        {
            if (!TryGetCurrentUserId(out var professorId))
            {
                return Unauthorized();
            }

            try
            {
                var record = _attendanceService.MarkAttendance(id, request.StudentId, request.Status, request.Justification, professorId);
                return Ok(MapRecord(record));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        private static AttendanceSessionResponseDto MapSession(Domain.Entities.AttendanceSession session)
        {
            return new AttendanceSessionResponseDto
            {
                Id = session.Id,
                ClassGroupId = session.ClassGroupId,
                Date = session.Date,
                CreatedAt = session.CreatedAt
            };
        }

        private static AttendanceRecordResponseDto MapRecord(Domain.Entities.AttendanceRecord record)
        {
            return new AttendanceRecordResponseDto
            {
                Id = record.Id,
                AttendanceSessionId = record.AttendanceSessionId,
                StudentId = record.StudentId,
                Status = record.Status,
                Justification = record.Justification,
                UpdatedAt = record.UpdatedAt
            };
        }

        private bool TryGetCurrentUserId(out Guid userId)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Guid.TryParse(userIdClaim, out userId);
        }
    }
}
