using Lisport.API.Domain.Entities;
using Lisport.API.Domain.Interfaces;

namespace Lisport.API.Application.Services
{
    public class EvolutionService : IEvolutionService
    {
        private readonly IEvolutionRepository _evolutionRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IClassRepository _classRepository;

        public EvolutionService(
            IEvolutionRepository evolutionRepository,
            IStudentRepository studentRepository,
            IClassRepository classRepository)
        {
            _evolutionRepository = evolutionRepository;
            _studentRepository = studentRepository;
            _classRepository = classRepository;
        }

        public StudentEvolution Create(
            Guid studentId,
            DateTime date,
            int physicalScore,
            int technicalScore,
            int behaviorScore,
            string? notes,
            Guid professorId)
        {
            ValidateScores(physicalScore, technicalScore, behaviorScore);

            var student = _studentRepository.GetById(studentId);
            if (student == null)
            {
                throw new InvalidOperationException("Aluno não encontrado.");
            }

            var classGroup = _classRepository.GetById(student.ClassGroupId);
            if (classGroup == null || classGroup.ProfessorId != professorId)
            {
                throw new InvalidOperationException("Aluno não pertence a este professor.");
            }

            var existing = _evolutionRepository.GetByStudentAndDate(studentId, date);
            if (existing != null)
            {
                throw new InvalidOperationException("Evolução já registrada para este aluno nesta data.");
            }

            var evolution = new StudentEvolution(studentId, date, physicalScore, technicalScore, behaviorScore, notes);
            _evolutionRepository.Add(evolution);
            return evolution;
        }

        public StudentEvolution? GetById(Guid id, Guid professorId)
        {
            var evolution = _evolutionRepository.GetById(id);
            if (evolution == null)
            {
                return null;
            }

            if (!IsStudentOwnedByProfessor(evolution.StudentId, professorId))
            {
                return null;
            }

            return evolution;
        }

        public IEnumerable<StudentEvolution> GetByStudent(Guid studentId, Guid professorId)
        {
            if (!IsStudentOwnedByProfessor(studentId, professorId))
            {
                return Enumerable.Empty<StudentEvolution>();
            }

            return _evolutionRepository.GetByStudent(studentId);
        }

        public StudentEvolution? Update(
            Guid id,
            Guid professorId,
            int? physicalScore,
            int? technicalScore,
            int? behaviorScore,
            string? notes)
        {
            var evolution = _evolutionRepository.GetById(id);
            if (evolution == null)
            {
                return null;
            }

            if (!IsStudentOwnedByProfessor(evolution.StudentId, professorId))
            {
                return null;
            }

            var newPhysical = physicalScore ?? evolution.PhysicalScore;
            var newTechnical = technicalScore ?? evolution.TechnicalScore;
            var newBehavior = behaviorScore ?? evolution.BehaviorScore;
            ValidateScores(newPhysical, newTechnical, newBehavior);

            evolution.UpdateScores(newPhysical, newTechnical, newBehavior, notes ?? evolution.Notes);
            _evolutionRepository.Update(evolution);
            return evolution;
        }

        public bool Delete(Guid id, Guid professorId)
        {
            var evolution = _evolutionRepository.GetById(id);
            if (evolution == null)
            {
                return false;
            }

            if (!IsStudentOwnedByProfessor(evolution.StudentId, professorId))
            {
                return false;
            }

            _evolutionRepository.Delete(evolution);
            return true;
        }

        private bool IsStudentOwnedByProfessor(Guid studentId, Guid professorId)
        {
            var student = _studentRepository.GetById(studentId);
            if (student == null)
            {
                return false;
            }

            var classGroup = _classRepository.GetById(student.ClassGroupId);
            return classGroup != null && classGroup.ProfessorId == professorId;
        }

        private static void ValidateScores(int physicalScore, int technicalScore, int behaviorScore)
        {
            if (!IsScoreValid(physicalScore) || !IsScoreValid(technicalScore) || !IsScoreValid(behaviorScore))
            {
                throw new InvalidOperationException("Scores devem estar entre 1 e 5.");
            }
        }

        private static bool IsScoreValid(int score)
        {
            return score >= 1 && score <= 5;
        }
    }
}
