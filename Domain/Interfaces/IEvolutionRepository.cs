using Lisport.API.Domain.Entities;

namespace Lisport.API.Domain.Interfaces
{
    public interface IEvolutionRepository
    {
        void Add(StudentEvolution evolution);
        StudentEvolution? GetById(Guid id);
        StudentEvolution? GetByStudentAndDate(Guid studentId, DateTime date);
        IEnumerable<StudentEvolution> GetByStudent(Guid studentId);
        void Update(StudentEvolution evolution);
        void Delete(StudentEvolution evolution);
    }
}
