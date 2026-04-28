namespace Lisport.API.Application.DTOs
{
    public class AttendanceSessionResponseDto
    {
        public Guid Id { get; set; }
        public Guid ClassGroupId { get; set; }
        public DateTime Date { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
