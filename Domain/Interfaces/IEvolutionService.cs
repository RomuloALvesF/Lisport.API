using Lisport.API.Domain.Entities;

namespace Lisport.API.Domain.Interfaces
{
    public interface IEvolutionService
    {
        StudentEvolution Create(
            Guid studentId,
            DateTime date,
            int physicalScore,
            int technicalScore,
            int behaviorScore,
            string? notes,
            Guid professorId);

        StudentEvolution? GetById(Guid id, Guid professorId);
        IEnumerable<StudentEvolution> GetByStudent(Guid studentId, Guid professorId);
        StudentEvolution? Update(
            Guid id,
            Guid professorId,
            int? physicalScore,
            int? technicalScore,
            int? behaviorScore,
            string? notes);

        bool Delete(Guid id, Guid professorId);
    }
}
