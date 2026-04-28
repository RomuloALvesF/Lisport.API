using Lisport.API.Domain.Entities;
using Lisport.API.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lisport.API.Infra.Data.Repository
{
    public class AlunoRepository : IAlunoRepository
    {
        private readonly AppDbContext _context;

        public AlunoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Aluno aluno)
        {
            _context.Alunos.Add(aluno);
            _context.SaveChanges();
        }

        public Aluno? GetById(Guid id)
        {
            return _context.Alunos.Find(id);
        }

        public IReadOnlyList<Aluno> GetByTurmaId(Guid turmaId)
        {
            return _context.Alunos.Where(a => a.TurmaId == turmaId).OrderBy(a => a.Nome).ToList();
        }

        public void Update(Aluno aluno)
        {
            _context.Alunos.Update(aluno);
            _context.SaveChanges();
        }

        public void Delete(Aluno aluno)
        {
            _context.Alunos.Remove(aluno);
            _context.SaveChanges();
        }
    }
}
