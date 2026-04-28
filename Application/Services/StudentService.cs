using Lisport.API.Domain.Entities;
using Lisport.API.Domain.Enums;
using Lisport.API.Domain.Interfaces;

namespace Lisport.API.Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IClassRepository _classRepository;

        public StudentService(IStudentRepository studentRepository, IClassRepository classRepository)
        {
            _studentRepository = studentRepository;
            _classRepository = classRepository;
        }

        public Student Create(
            string name,
            DateTime birthDate,
            Gender gender,
            string document,
            string responsibleName,
            string phone,
            string address,
            string? notes,
            Guid classGroupId,
            Guid professorId)
        {
            var classGroup = _classRepository.GetById(classGroupId);
            if (classGroup == null || classGroup.ProfessorId != professorId)
            {
                throw new InvalidOperationException("Turma não encontrada para este professor.");
            }

            var student = new Student(name, birthDate, gender, document, responsibleName, phone, address, notes, classGroupId);
            _studentRepository.Add(student);
            return student;
        }

        public Student? GetById(Guid id, Guid professorId)
        {
            var student = _studentRepository.GetById(id);
            if (student == null)
            {
                return null;
            }

            var classGroup = _classRepository.GetById(student.ClassGroupId);
            if (classGroup == null || classGroup.ProfessorId != professorId)
            {
                return null;
            }

            return student;
        }

        public IEnumerable<Student> GetByClass(Guid classGroupId, Guid professorId)
        {
            var classGroup = _classRepository.GetById(classGroupId);
            if (classGroup == null || classGroup.ProfessorId != professorId)
            {
                return Enumerable.Empty<Student>();
            }

            return _studentRepository.GetByClassId(classGroupId);
        }

        public Student? Update(
            Guid id,
            Guid professorId,
            string? name,
            DateTime? birthDate,
            Gender? gender,
            string? document,
            string? responsibleName,
            string? phone,
            string? address,
            string? notes)
        {
            var student = _studentRepository.GetById(id);
            if (student == null)
            {
                return null;
            }

            var classGroup = _classRepository.GetById(student.ClassGroupId);
            if (classGroup == null || classGroup.ProfessorId != professorId)
            {
                return null;
            }

            if (!string.IsNullOrWhiteSpace(name))
            {
                student.UpdateName(name);
            }

            if (birthDate.HasValue)
            {
                student.UpdateBirthDate(birthDate.Value);
            }

            if (gender.HasValue)
            {
                student.UpdateGender(gender.Value);
            }

            if (!string.IsNullOrWhiteSpace(document))
            {
                student.UpdateDocument(document);
            }

            if (!string.IsNullOrWhiteSpace(responsibleName))
            {
                student.UpdateResponsibleName(responsibleName);
            }

            if (!string.IsNullOrWhiteSpace(phone))
            {
                student.UpdatePhone(phone);
            }

            if (!string.IsNullOrWhiteSpace(address))
            {
                student.UpdateAddress(address);
            }

            if (notes != null)
            {
                student.UpdateNotes(notes);
            }

            _studentRepository.Update(student);
            return student;
        }

        public bool Delete(Guid id, Guid professorId)
        {
            var student = _studentRepository.GetById(id);
            if (student == null)
            {
                return false;
            }

            var classGroup = _classRepository.GetById(student.ClassGroupId);
            if (classGroup == null || classGroup.ProfessorId != professorId)
            {
                return false;
            }

            _studentRepository.Delete(student);
            return true;
        }
    }
}
