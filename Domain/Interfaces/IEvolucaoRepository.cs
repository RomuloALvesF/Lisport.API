using Lisport.API.Domain.Entities;

namespace Lisport.API.Domain.Interfaces
{
    public interface IEvolucaoRepository
    {
        void Add(Evolucao evolucao);
        Evolucao? GetById(Guid id);
        Evolucao? GetByAlunoAndPeriodo(Guid alunoId, string periodo);
        IReadOnlyList<Evolucao> GetByAlunoId(Guid alunoId);
        IReadOnlyList<Evolucao> GetByPeriodo(string periodo);
        string? GetUltimoPeriodo();
        void Update(Evolucao evolucao);
        void SaveChanges();
    }
}
