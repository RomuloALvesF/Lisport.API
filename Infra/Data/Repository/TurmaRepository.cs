using Lisport.API.Domain.Entities;
using Lisport.API.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lisport.API.Infra.Data.Repository
{
    public class TurmaRepository : ITurmaRepository
    {
        private readonly AppDbContext _context;

        public TurmaRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Turma turma)
        {
            _context.Turmas.Add(turma);
            _context.SaveChanges();
        }

        public Turma? GetById(Guid id)
        {
            return _context.Turmas.Find(id);
        }

        public IReadOnlyList<Turma> GetAll()
        {
            return _context.Turmas.OrderBy(t => t.Nome).ToList();
        }

        public void Update(Turma turma)
        {
            _context.Turmas.Update(turma);
            _context.SaveChanges();
        }

        public void Delete(Turma turma)
        {
            _context.Turmas.Remove(turma);
            _context.SaveChanges();
        }
    }
}
