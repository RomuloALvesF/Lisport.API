using Lisport.API.Domain.Entities;

namespace Lisport.API.Domain.Interfaces
{
    public interface IAttendanceRecordRepository
    {
        void AddRange(IEnumerable<AttendanceRecord> records);
        AttendanceRecord? GetById(Guid id);
        AttendanceRecord? GetBySessionAndStudent(Guid sessionId, Guid studentId);
        IEnumerable<AttendanceRecord> GetBySessionId(Guid sessionId);
        void Update(AttendanceRecord record);
    }
}
