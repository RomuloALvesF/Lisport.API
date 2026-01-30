namespace Lisport.API.Domain.Entities
{
    public class AttendanceSession
    {
        public Guid Id { get; private set; }
        public Guid ClassGroupId { get; private set; }
        public ClassGroup ClassGroup { get; private set; } = null!;
        public DateTime Date { get; private set; }
        public ICollection<AttendanceRecord> Records { get; private set; } = new List<AttendanceRecord>();
        public DateTime CreatedAt { get; private set; }

        private AttendanceSession() { }

        public AttendanceSession(Guid classGroupId, DateTime date)
        {
            Id = Guid.NewGuid();
            ClassGroupId = classGroupId;
            Date = date.Date;
            CreatedAt = DateTime.Now;
        }
    }
}
