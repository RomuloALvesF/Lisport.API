using Lisport.API.Domain.Entities;
using Lisport.API.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lisport.API.Infra.Data.Repository
{
    public class ObservacaoGeralRepository : IObservacaoGeralRepository
    {
        private readonly AppDbContext _context;

        public ObservacaoGeralRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(ObservacaoGeral observacao)
        {
            _context.ObservacoesGerais.Add(observacao);
            _context.SaveChanges();
        }

        public IReadOnlyList<ObservacaoGeral> GetFiltered(Guid? turmaId, Guid? alunoId)
        {
            var q = _context.ObservacoesGerais.AsQueryable();
            if (turmaId.HasValue) q = q.Where(o => o.TurmaId == turmaId);
            if (alunoId.HasValue) q = q.Where(o => o.AlunoId == alunoId);
            return q.OrderByDescending(o => o.CreatedAt).ToList();
        }
    }
}
