namespace Lisport.API.Domain.Entities
{
    public class ClassGroup
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Modality { get; private set; } = string.Empty;
        public string AgeRange { get; private set; } = string.Empty;
        public string DaysAndTimes { get; private set; } = string.Empty;
        public Guid ProfessorId { get; private set; }
        public User Professor { get; private set; } = null!;
        public ICollection<Student> Students { get; private set; } = new List<Student>();
        public DateTime CreatedAt { get; private set; }

        private ClassGroup() { }

        public ClassGroup(string name, string modality, string ageRange, string daysAndTimes, Guid professorId)
        {
            Id = Guid.NewGuid();
            Name = name;
            Modality = modality;
            AgeRange = ageRange;
            DaysAndTimes = daysAndTimes;
            ProfessorId = professorId;
            CreatedAt = DateTime.Now;
        }

        public void UpdateName(string name)
        {
            Name = name;
        }

        public void UpdateModality(string modality)
        {
            Modality = modality;
        }

        public void UpdateAgeRange(string ageRange)
        {
            AgeRange = ageRange;
        }

        public void UpdateDaysAndTimes(string daysAndTimes)
        {
            DaysAndTimes = daysAndTimes;
        }
    }
}
