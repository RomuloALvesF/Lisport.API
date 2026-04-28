using Lisport.API.Domain.Entities;
using Lisport.API.Domain.Enums;
using Lisport.API.Domain.Interfaces;

namespace Lisport.API.Application.Services
{
    public class EvolucaoService : IEvolucaoService
    {
        private readonly IEvolucaoRepository _evolucaoRepository;
        private readonly IAlunoRepository _alunoRepository;

        public EvolucaoService(IEvolucaoRepository evolucaoRepository, IAlunoRepository alunoRepository)
        {
            _evolucaoRepository = evolucaoRepository;
            _alunoRepository = alunoRepository;
        }

        public Evolucao Upsert(Guid alunoId, string periodo, NivelEvolucao evolucaoFisica, NivelEvolucao evolucaoTecnica, NivelEvolucao comportamento, string? observacao)
        {
            if (_alunoRepository.GetById(alunoId) == null)
                throw new InvalidOperationException("Aluno não encontrado.");
            var existing = _evolucaoRepository.GetByAlunoAndPeriodo(alunoId, periodo);
            if (existing != null)
            {
                existing.Update(evolucaoFisica, evolucaoTecnica, comportamento, observacao);
                _evolucaoRepository.Update(existing);
                return existing;
            }
            var evolucao = new Evolucao(alunoId, periodo, evolucaoFisica, evolucaoTecnica, comportamento, observacao);
            _evolucaoRepository.Add(evolucao);
            return evolucao;
        }

        public Evolucao? GetByAlunoAndPeriodo(Guid alunoId, string periodo)
        {
            return _evolucaoRepository.GetByAlunoAndPeriodo(alunoId, periodo);
        }

        public IReadOnlyList<Evolucao> GetByAlunoId(Guid alunoId)
        {
            return _evolucaoRepository.GetByAlunoId(alunoId);
        }
    }
}
