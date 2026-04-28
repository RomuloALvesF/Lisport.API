namespace Lisport.API.Application.DTOs
{
    public class ClassResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Modality { get; set; }
        public string AgeRange { get; set; }
        public string DaysAndTimes { get; set; }
        public Guid ProfessorId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
