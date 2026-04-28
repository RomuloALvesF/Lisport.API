using Lisport.API.Domain.Entities;

namespace Lisport.API.Domain.Interfaces
{
    public interface IStudentRepository
    {
        void Add(Student student);
        Student? GetById(Guid id);
        IEnumerable<Student> GetByClassId(Guid classGroupId);
        void Update(Student student);
        void Delete(Student student);
    }
}
