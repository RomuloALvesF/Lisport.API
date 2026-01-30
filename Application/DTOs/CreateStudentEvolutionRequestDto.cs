using System.ComponentModel.DataAnnotations;

namespace Lisport.API.Application.DTOs
{
    public class CreateStudentEvolutionRequestDto
    {
        [Required]
        public Guid StudentId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Range(1, 5)]
        public int PhysicalScore { get; set; }

        [Range(1, 5)]
        public int TechnicalScore { get; set; }

        [Range(1, 5)]
        public int BehaviorScore { get; set; }

        public string? Notes { get; set; }
    }
}
