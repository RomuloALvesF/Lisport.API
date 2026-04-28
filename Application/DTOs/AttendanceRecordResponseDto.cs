using Lisport.API.Domain.Enums;

namespace Lisport.API.Application.DTOs
{
    public class AttendanceRecordResponseDto
    {
        public Guid Id { get; set; }
        public Guid AttendanceSessionId { get; set; }
        public Guid StudentId { get; set; }
        public AttendanceStatus Status { get; set; }
        public string? Justification { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
