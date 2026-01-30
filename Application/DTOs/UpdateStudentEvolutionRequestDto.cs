using System.ComponentModel.DataAnnotations;

namespace Lisport.API.Application.DTOs
{
    public class UpdateStudentEvolutionRequestDto
    {
        [Range(1, 5)]
        public int? PhysicalScore { get; set; }

        [Range(1, 5)]
        public int? TechnicalScore { get; set; }

        [Range(1, 5)]
        public int? BehaviorScore { get; set; }

        public string? Notes { get; set; }
    }
}
