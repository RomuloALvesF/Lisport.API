using Lisport.API.Domain.Entities;
using Lisport.API.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lisport.API.Infra.Data.Repository
{
    public class ClassRepository : IClassRepository
    {
        private readonly AppDbContext _context;

        public ClassRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(ClassGroup classGroup)
        {
            _context.ClassGroups.Add(classGroup);
            _context.SaveChanges();
        }

        public ClassGroup? GetById(Guid id)
        {
            return _context.ClassGroups.FirstOrDefault(c => c.Id == id);
        }

        public ClassGroup? GetByIdWithStudents(Guid id)
        {
            return _context.ClassGroups
                .Include(c => c.Students)
                .FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<ClassGroup> GetByProfessorId(Guid professorId)
        {
            return _context.ClassGroups
                .Where(c => c.ProfessorId == professorId)
                .OrderBy(c => c.Name)
                .ToList();
        }

        public void Update(ClassGroup classGroup)
        {
            _context.ClassGroups.Update(classGroup);
            _context.SaveChanges();
        }

        public void Delete(ClassGroup classGroup)
        {
            _context.ClassGroups.Remove(classGroup);
            _context.SaveChanges();
        }
    }
}
