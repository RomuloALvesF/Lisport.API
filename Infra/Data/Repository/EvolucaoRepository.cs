using Lisport.API.Domain.Entities;
using Lisport.API.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lisport.API.Infra.Data.Repository
{
    public class EvolucaoRepository : IEvolucaoRepository
    {
        private readonly AppDbContext _context;

        public EvolucaoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Evolucao evolucao)
        {
            _context.Evolucoes.Add(evolucao);
            _context.SaveChanges();
        }

        public Evolucao? GetById(Guid id)
        {
            return _context.Evolucoes.Find(id);
        }

        public Evolucao? GetByAlunoAndPeriodo(Guid alunoId, string periodo)
        {
            return _context.Evolucoes.FirstOrDefault(e => e.AlunoId == alunoId && e.Periodo == periodo);
        }

        public IReadOnlyList<Evolucao> GetByAlunoId(Guid alunoId)
        {
            return _context.Evolucoes.Where(e => e.AlunoId == alunoId).OrderByDescending(e => e.Periodo).ToList();
        }

        public IReadOnlyList<Evolucao> GetByPeriodo(string periodo)
        {
            return _context.Evolucoes.Where(e => e.Periodo == periodo).ToList();
        }

        public string? GetUltimoPeriodo()
        {
            return _context.Evolucoes.OrderByDescending(e => e.Periodo).Select(e => e.Periodo).FirstOrDefault();
        }

        public void Update(Evolucao evolucao)
        {
            _context.Evolucoes.Update(evolucao);
            _context.SaveChanges();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
