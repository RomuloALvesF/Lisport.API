using Lisport.API.Domain.Entities;
using Lisport.API.Domain.Enums;

namespace Lisport.API.Domain.Interfaces
{
    public interface IAttendanceService
    {
        AttendanceSession CreateSession(Guid classGroupId, DateTime date, Guid professorId);
        AttendanceSession? GetSession(Guid sessionId, Guid professorId);
        IEnumerable<AttendanceSession> GetSessionsByClass(Guid classGroupId, Guid professorId);
        IEnumerable<AttendanceRecord> GetRecords(Guid sessionId, Guid professorId);
        AttendanceRecord MarkAttendance(Guid sessionId, Guid studentId, AttendanceStatus status, string? justification, Guid professorId);
    }
}
