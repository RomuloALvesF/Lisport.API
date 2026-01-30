using Lisport.API.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Lisport.API.Application.DTOs
{
    public class MarkAttendanceRequestDto
    {
        [Required]
        public Guid StudentId { get; set; }

        [Required]
        public AttendanceStatus Status { get; set; }

        public string? Justification { get; set; }
    }
}
