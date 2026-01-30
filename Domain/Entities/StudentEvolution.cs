namespace Lisport.API.Domain.Entities
{
    public class StudentEvolution
    {
        public Guid Id { get; private set; }
        public Guid StudentId { get; private set; }
        public Student Student { get; private set; } = null!;
        public DateTime Date { get; private set; }
        public int PhysicalScore { get; private set; }
        public int TechnicalScore { get; private set; }
        public int BehaviorScore { get; private set; }
        public string? Notes { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        private StudentEvolution() { }

        public StudentEvolution(
            Guid studentId,
            DateTime date,
            int physicalScore,
            int technicalScore,
            int behaviorScore,
            string? notes)
        {
            Id = Guid.NewGuid();
            StudentId = studentId;
            Date = date.Date;
            PhysicalScore = physicalScore;
            TechnicalScore = technicalScore;
            BehaviorScore = behaviorScore;
            Notes = notes;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }

        public void UpdateScores(int physicalScore, int technicalScore, int behaviorScore, string? notes)
        {
            PhysicalScore = physicalScore;
            TechnicalScore = technicalScore;
            BehaviorScore = behaviorScore;
            Notes = notes;
            UpdatedAt = DateTime.Now;
        }
    }
}
