using Lisport.API.Domain.Enums;

namespace Lisport.API.Domain.Entities
{
    public class AttendanceRecord
    {
        public Guid Id { get; private set; }
        public Guid AttendanceSessionId { get; private set; }
        public AttendanceSession AttendanceSession { get; private set; } = null!;
        public Guid StudentId { get; private set; }
        public Student Student { get; private set; } = null!;
        public AttendanceStatus Status { get; private set; }
        public string? Justification { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        private AttendanceRecord() { }

        public AttendanceRecord(Guid attendanceSessionId, Guid studentId)
        {
            Id = Guid.NewGuid();
            AttendanceSessionId = attendanceSessionId;
            StudentId = studentId;
            Status = AttendanceStatus.NotMarked;
            UpdatedAt = DateTime.Now;
        }

        public void UpdateStatus(AttendanceStatus status, string? justification)
        {
            Status = status;
            Justification = justification;
            UpdatedAt = DateTime.Now;
        }
    }
}
