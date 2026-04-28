using Lisport.API.Domain.Entities;

namespace Lisport.API.Domain.Interfaces
{
    public interface IPresencaRepository
    {
        void Add(Presenca presenca);
        Presenca? GetByTurmaDataAluno(Guid turmaId, DateTime data, Guid alunoId);
        IReadOnlyList<Presenca> GetByTurmaAndData(Guid turmaId, DateTime data);
        (int TotalRegistros, int TotalPresentes) GetStatsByPeriodo(DateTime de, DateTime ate);
        void Update(Presenca presenca);
        void SaveChanges();
    }
}
