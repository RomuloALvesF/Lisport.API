namespace Lisport.API.Application.DTOs
{
    public class StudentEvolutionResponseDto
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public DateTime Date { get; set; }
        public int PhysicalScore { get; set; }
        public int TechnicalScore { get; set; }
        public int BehaviorScore { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
