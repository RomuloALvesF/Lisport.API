using Lisport.API.Domain.Entities;
using Lisport.API.Domain.Enums;
using Lisport.API.Domain.Interfaces;

namespace Lisport.API.Application.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceSessionRepository _sessionRepository;
        private readonly IAttendanceRecordRepository _recordRepository;
        private readonly IClassRepository _classRepository;
        private readonly IStudentRepository _studentRepository;

        public AttendanceService(
            IAttendanceSessionRepository sessionRepository,
            IAttendanceRecordRepository recordRepository,
            IClassRepository classRepository,
            IStudentRepository studentRepository)
        {
            _sessionRepository = sessionRepository;
            _recordRepository = recordRepository;
            _classRepository = classRepository;
            _studentRepository = studentRepository;
        }

        public AttendanceSession CreateSession(Guid classGroupId, DateTime date, Guid professorId)
        {
            var classGroup = _classRepository.GetById(classGroupId);
            if (classGroup == null || classGroup.ProfessorId != professorId)
            {
                throw new InvalidOperationException("Turma não encontrada para este professor.");
            }

            var existing = _sessionRepository.GetByClassAndDate(classGroupId, date);
            if (existing != null)
            {
                return existing;
            }

            var session = new AttendanceSession(classGroupId, date);
            _sessionRepository.Add(session);

            var students = _studentRepository.GetByClassId(classGroupId);
            var records = students.Select(s => new AttendanceRecord(session.Id, s.Id)).ToList();
            if (records.Count > 0)
            {
                _recordRepository.AddRange(records);
            }

            return session;
        }

        public AttendanceSession? GetSession(Guid sessionId, Guid professorId)
        {
            var session = _sessionRepository.GetById(sessionId);
            if (session == null)
            {
                return null;
            }

            var classGroup = _classRepository.GetById(session.ClassGroupId);
            if (classGroup == null || classGroup.ProfessorId != professorId)
            {
                return null;
            }

            return session;
        }

        public IEnumerable<AttendanceSession> GetSessionsByClass(Guid classGroupId, Guid professorId)
        {
            var classGroup = _classRepository.GetById(classGroupId);
            if (classGroup == null || classGroup.ProfessorId != professorId)
            {
                return Enumerable.Empty<AttendanceSession>();
            }

            return _sessionRepository.GetByClassId(classGroupId);
        }

        public IEnumerable<AttendanceRecord> GetRecords(Guid sessionId, Guid professorId)
        {
            var session = _sessionRepository.GetById(sessionId);
            if (session == null)
            {
                return Enumerable.Empty<AttendanceRecord>();
            }

            var classGroup = _classRepository.GetById(session.ClassGroupId);
            if (classGroup == null || classGroup.ProfessorId != professorId)
            {
                return Enumerable.Empty<AttendanceRecord>();
            }

            return _recordRepository.GetBySessionId(sessionId);
        }

        public AttendanceRecord MarkAttendance(Guid sessionId, Guid studentId, AttendanceStatus status, string? justification, Guid professorId)
        {
            var session = _sessionRepository.GetById(sessionId);
            if (session == null)
            {
                throw new InvalidOperationException("Sessão de presença não encontrada.");
            }

            var classGroup = _classRepository.GetById(session.ClassGroupId);
            if (classGroup == null || classGroup.ProfessorId != professorId)
            {
                throw new InvalidOperationException("Sessão não pertence a este professor.");
            }

            if (status == AttendanceStatus.JustifiedAbsent && string.IsNullOrWhiteSpace(justification))
            {
                throw new InvalidOperationException("Justificativa é obrigatória para falta justificada.");
            }

            var record = _recordRepository.GetBySessionAndStudent(sessionId, studentId);
            if (record == null)
            {
                throw new InvalidOperationException("Aluno não encontrado nesta sessão.");
            }

            record.UpdateStatus(status, justification);
            _recordRepository.Update(record);
            return record;
        }
    }
}
