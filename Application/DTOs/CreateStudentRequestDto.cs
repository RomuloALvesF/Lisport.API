using Lisport.API.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Lisport.API.Application.DTOs
{
    public class CreateStudentRequestDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required]
        public string Document { get; set; }

        [Required]
        public string ResponsibleName { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Address { get; set; }

        public string? Notes { get; set; }

        [Required]
        public Guid ClassGroupId { get; set; }
    }
}
