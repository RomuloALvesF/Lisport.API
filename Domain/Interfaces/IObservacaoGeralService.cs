using Lisport.API.Domain.Entities;

namespace Lisport.API.Domain.Interfaces
{
    public interface IObservacaoGeralService
    {
        ObservacaoGeral Create(string texto, Guid createdByUserId, Guid? turmaId = null, Guid? alunoId = null);
        IReadOnlyList<ObservacaoGeral> GetFiltered(Guid? turmaId, Guid? alunoId);
    }
}
