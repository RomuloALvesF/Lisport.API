using Lisport.API.Domain.Entities;
using Lisport.API.Domain.Interfaces;
using Lisport.API.Infra.Data;

namespace Lisport.API.Infra.Data.Repository
{
    public class EvolutionRepository : IEvolutionRepository
    {
        private readonly AppDbContext _context;

        public EvolutionRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(StudentEvolution evolution)
        {
            _context.StudentEvolutions.Add(evolution);
            _context.SaveChanges();
        }

        public StudentEvolution? GetById(Guid id)
        {
            return _context.StudentEvolutions.FirstOrDefault(e => e.Id == id);
        }

        public StudentEvolution? GetByStudentAndDate(Guid studentId, DateTime date)
        {
            var normalizedDate = date.Date;
            return _context.StudentEvolutions.FirstOrDefault(e => e.StudentId == studentId && e.Date == normalizedDate);
        }

        public IEnumerable<StudentEvolution> GetByStudent(Guid studentId)
        {
            return _context.StudentEvolutions
                .Where(e => e.StudentId == studentId)
                .OrderByDescending(e => e.Date)
                .ToList();
        }

        public void Update(StudentEvolution evolution)
        {
            _context.StudentEvolutions.Update(evolution);
            _context.SaveChanges();
        }

        public void Delete(StudentEvolution evolution)
        {
            _context.StudentEvolutions.Remove(evolution);
            _context.SaveChanges();
        }
    }
}
