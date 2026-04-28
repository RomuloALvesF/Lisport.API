using Lisport.API.Domain.Entities;

namespace Lisport.API.Domain.Interfaces
{
    public interface IClassRepository
    {
        void Add(ClassGroup classGroup);
        ClassGroup? GetById(Guid id);
        ClassGroup? GetByIdWithStudents(Guid id);
        IEnumerable<ClassGroup> GetByProfessorId(Guid professorId);
        void Update(ClassGroup classGroup);
        void Delete(ClassGroup classGroup);
    }
}
