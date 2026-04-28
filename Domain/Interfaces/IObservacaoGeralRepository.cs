using Lisport.API.Domain.Entities;

namespace Lisport.API.Domain.Interfaces
{
    public interface IObservacaoGeralRepository
    {
        void Add(ObservacaoGeral observacao);
        IReadOnlyList<ObservacaoGeral> GetFiltered(Guid? turmaId, Guid? alunoId);
    }
}
