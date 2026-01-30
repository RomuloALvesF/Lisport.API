using Lisport.API.Domain.Entities;

namespace Lisport.API.Domain.Interfaces
{
    public interface IClassService
    {
        ClassGroup Create(string name, string modality, string ageRange, string daysAndTimes, Guid professorId);
        ClassGroup? GetById(Guid id, Guid professorId);
        IEnumerable<ClassGroup> GetByProfessor(Guid professorId);
        ClassGroup? Update(Guid id, Guid professorId, string? name, string? modality, string? ageRange, string? daysAndTimes);
        bool Delete(Guid id, Guid professorId);
    }
}
