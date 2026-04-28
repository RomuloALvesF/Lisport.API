using Lisport.API.Domain.Entities;

namespace Lisport.API.Domain.Interfaces
{
    public interface IStudentService
    {
        Student Create(
            string name,
            DateTime birthDate,
            Domain.Enums.Gender gender,
            string document,
            string responsibleName,
            string phone,
            string address,
            string? notes,
            Guid classGroupId,
            Guid professorId);

        Student? GetById(Guid id, Guid professorId);
        IEnumerable<Student> GetByClass(Guid classGroupId, Guid professorId);
        Student? Update(
            Guid id,
            Guid professorId,
            string? name,
            DateTime? birthDate,
            Domain.Enums.Gender? gender,
            string? document,
            string? responsibleName,
            string? phone,
            string? address,
            string? notes);

        bool Delete(Guid id, Guid professorId);
    }
}
