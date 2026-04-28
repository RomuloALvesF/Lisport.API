using Lisport.API.Domain.Entities;
using Lisport.API.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lisport.API.Infra.Data.Repository
{
    public class PresencaRepository : IPresencaRepository
    {
        private readonly AppDbContext _context;

        public PresencaRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Presenca presenca)
        {
            _context.Presencas.Add(presenca);
        }

        public Presenca? GetByTurmaDataAluno(Guid turmaId, DateTime data, Guid alunoId)
        {
            var dataOnly = data.Date;
            return _context.Presencas.FirstOrDefault(p =>
                p.TurmaId == turmaId && p.Data == dataOnly && p.AlunoId == alunoId);
        }

        public IReadOnlyList<Presenca> GetByTurmaAndData(Guid turmaId, DateTime data)
        {
            var dataOnly = data.Date;
            return _context.Presencas.Where(p => p.TurmaId == turmaId && p.Data == dataOnly).ToList();
        }

        public (int TotalRegistros, int TotalPresentes) GetStatsByPeriodo(DateTime de, DateTime ate)
        {
            var di = de.Date;
            var df = ate.Date;
            var q = _context.Presencas.Where(p => p.Data >= di && p.Data <= df);
            var total = q.Count();
            var presentes = q.Count(p => p.Presente);
            return (total, presentes);
        }

        public void Update(Presenca presenca)
        {
            _context.Presencas.Update(presenca);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
