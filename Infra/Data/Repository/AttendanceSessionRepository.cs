using Lisport.API.Domain.Entities;
using Lisport.API.Domain.Interfaces;

namespace Lisport.API.Infra.Data.Repository
{
    public class AttendanceSessionRepository : IAttendanceSessionRepository
    {
        private readonly AppDbContext _context;

        public AttendanceSessionRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(AttendanceSession session)
        {
            _context.AttendanceSessions.Add(session);
            _context.SaveChanges();
        }

        public AttendanceSession? GetById(Guid id)
        {
            return _context.AttendanceSessions.FirstOrDefault(s => s.Id == id);
        }

        public AttendanceSession? GetByClassAndDate(Guid classGroupId, DateTime date)
        {
            var normalizedDate = date.Date;
            return _context.AttendanceSessions.FirstOrDefault(s => s.ClassGroupId == classGroupId && s.Date == normalizedDate);
        }

        public IEnumerable<AttendanceSession> GetByClassId(Guid classGroupId)
        {
            return _context.AttendanceSessions
                .Where(s => s.ClassGroupId == classGroupId)
                .OrderByDescending(s => s.Date)
                .ToList();
        }
    }
}
