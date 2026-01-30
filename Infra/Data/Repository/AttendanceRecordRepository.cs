using Lisport.API.Domain.Entities;
using Lisport.API.Domain.Interfaces;

namespace Lisport.API.Infra.Data.Repository
{
    public class AttendanceRecordRepository : IAttendanceRecordRepository
    {
        private readonly AppDbContext _context;

        public AttendanceRecordRepository(AppDbContext context)
        {
            _context = context;
        }

        public void AddRange(IEnumerable<AttendanceRecord> records)
        {
            _context.AttendanceRecords.AddRange(records);
            _context.SaveChanges();
        }

        public AttendanceRecord? GetById(Guid id)
        {
            return _context.AttendanceRecords.FirstOrDefault(r => r.Id == id);
        }

        public AttendanceRecord? GetBySessionAndStudent(Guid sessionId, Guid studentId)
        {
            return _context.AttendanceRecords.FirstOrDefault(r => r.AttendanceSessionId == sessionId && r.StudentId == studentId);
        }

        public IEnumerable<AttendanceRecord> GetBySessionId(Guid sessionId)
        {
            return _context.AttendanceRecords
                .Where(r => r.AttendanceSessionId == sessionId)
                .ToList();
        }

        public void Update(AttendanceRecord record)
        {
            _context.AttendanceRecords.Update(record);
            _context.SaveChanges();
        }
    }
}
