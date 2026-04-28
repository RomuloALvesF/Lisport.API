using System.ComponentModel.DataAnnotations;

namespace Lisport.API.Application.DTOs
{
    public class CreateAttendanceSessionRequestDto
    {
        [Required]
        public Guid ClassGroupId { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
