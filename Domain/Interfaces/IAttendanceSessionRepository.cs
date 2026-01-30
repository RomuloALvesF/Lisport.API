using Lisport.API.Domain.Entities;

namespace Lisport.API.Domain.Interfaces
{
    public interface IAttendanceSessionRepository
    {
        void Add(AttendanceSession session);
        AttendanceSession? GetById(Guid id);
        AttendanceSession? GetByClassAndDate(Guid classGroupId, DateTime date);
        IEnumerable<AttendanceSession> GetByClassId(Guid classGroupId);
    }
}
