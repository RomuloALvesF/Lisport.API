using Lisport.API.Domain.Enums;

namespace Lisport.API.Application.DTOs
{
    public class UpdateStudentRequestDto
    {
        public string? Name { get; set; }
        public DateTime? BirthDate { get; set; }
        public Gender? Gender { get; set; }
        public string? Document { get; set; }
        public string? ResponsibleName { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? Notes { get; set; }
    }
}
