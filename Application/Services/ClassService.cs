using Lisport.API.Domain.Entities;
using Lisport.API.Domain.Interfaces;

namespace Lisport.API.Application.Services
{
    public class ClassService : IClassService
    {
        private readonly IClassRepository _classRepository;

        public ClassService(IClassRepository classRepository)
        {
            _classRepository = classRepository;
        }

        public ClassGroup Create(string name, string modality, string ageRange, string daysAndTimes, Guid professorId)
        {
            var classGroup = new ClassGroup(name, modality, ageRange, daysAndTimes, professorId);
            _classRepository.Add(classGroup);
            return classGroup;
        }

        public ClassGroup? GetById(Guid id, Guid professorId)
        {
            var classGroup = _classRepository.GetById(id);
            if (classGroup == null || classGroup.ProfessorId != professorId)
            {
                return null;
            }

            return classGroup;
        }

        public IEnumerable<ClassGroup> GetByProfessor(Guid professorId)
        {
            return _classRepository.GetByProfessorId(professorId);
        }

        public ClassGroup? Update(Guid id, Guid professorId, string? name, string? modality, string? ageRange, string? daysAndTimes)
        {
            var classGroup = _classRepository.GetById(id);
            if (classGroup == null || classGroup.ProfessorId != professorId)
            {
                return null;
            }

            if (!string.IsNullOrWhiteSpace(name))
            {
                classGroup.UpdateName(name);
            }

            if (!string.IsNullOrWhiteSpace(modality))
            {
                classGroup.UpdateModality(modality);
            }

            if (!string.IsNullOrWhiteSpace(ageRange))
            {
                classGroup.UpdateAgeRange(ageRange);
            }

            if (!string.IsNullOrWhiteSpace(daysAndTimes))
            {
                classGroup.UpdateDaysAndTimes(daysAndTimes);
            }

            _classRepository.Update(classGroup);
            return classGroup;
        }

        public bool Delete(Guid id, Guid professorId)
        {
            var classGroup = _classRepository.GetById(id);
            if (classGroup == null || classGroup.ProfessorId != professorId)
            {
                return false;
            }

            _classRepository.Delete(classGroup);
            return true;
        }
    }
}
