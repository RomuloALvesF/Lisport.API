using Lisport.API.Domain.Entities;

namespace Lisport.API.Domain.Interfaces
{
    public interface IEvolucaoService
    {
        Evolucao Upsert(Guid alunoId, string periodo, Domain.Enums.NivelEvolucao evolucaoFisica, Domain.Enums.NivelEvolucao evolucaoTecnica, Domain.Enums.NivelEvolucao comportamento, string? observacao);
        Evolucao? GetByAlunoAndPeriodo(Guid alunoId, string periodo);
        IReadOnlyList<Evolucao> GetByAlunoId(Guid alunoId);
    }
}
