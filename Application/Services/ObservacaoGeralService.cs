using Lisport.API.Domain.Entities;
using Lisport.API.Domain.Interfaces;

namespace Lisport.API.Application.Services
{
    public class ObservacaoGeralService : IObservacaoGeralService
    {
        private readonly IObservacaoGeralRepository _repository;

        public ObservacaoGeralService(IObservacaoGeralRepository repository)
        {
            _repository = repository;
        }

        public ObservacaoGeral Create(string texto, Guid createdByUserId, Guid? turmaId = null, Guid? alunoId = null)
        {
            var obs = new ObservacaoGeral(texto, createdByUserId, turmaId, alunoId);
            _repository.Add(obs);
            return obs;
        }

        public IReadOnlyList<ObservacaoGeral> GetFiltered(Guid? turmaId, Guid? alunoId)
        {
            return _repository.GetFiltered(turmaId, alunoId);
        }
    }
}
