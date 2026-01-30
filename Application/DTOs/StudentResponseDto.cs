using Lisport.API.Domain.Enums;

namespace Lisport.API.Application.DTOs
{
    public class StudentResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public string Document { get; set; }
        public string ResponsibleName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string? Notes { get; set; }
        public Guid ClassGroupId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
