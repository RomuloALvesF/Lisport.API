using Lisport.API.Domain.Entities;
using Lisport.API.Domain.Interfaces;

namespace Lisport.API.Application.Services
{
    public class TurmaService : ITurmaService
    {
        private readonly ITurmaRepository _repository;

        public TurmaService(ITurmaRepository repository)
        {
            _repository = repository;
        }

        public Turma Create(string nome, string modalidade, string faixaEtaria, string diasHorarios)
        {
            var turma = new Turma(nome, modalidade, faixaEtaria, diasHorarios);
            _repository.Add(turma);
            return turma;
        }

        public Turma? GetById(Guid id) => _repository.GetById(id);

        public IReadOnlyList<Turma> GetAll() => _repository.GetAll();

        public Turma? Update(Guid id, string nome, string modalidade, string faixaEtaria, string diasHorarios)
        {
            var turma = _repository.GetById(id);
            if (turma == null) return null;
            turma.Update(nome, modalidade, faixaEtaria, diasHorarios);
            _repository.Update(turma);
            return turma;
        }

        public bool Delete(Guid id)
        {
            var turma = _repository.GetById(id);
            if (turma == null) return false;
            _repository.Delete(turma);
            return true;
        }
    }
}
