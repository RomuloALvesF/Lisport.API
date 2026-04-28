using Lisport.API.Domain.Entities;
using Lisport.API.Domain.Interfaces;

namespace Lisport.API.Application.Services
{
    public class AlunoService : IAlunoService
    {
        private readonly IAlunoRepository _alunoRepository;
        private readonly ITurmaRepository _turmaRepository;

        public AlunoService(IAlunoRepository alunoRepository, ITurmaRepository turmaRepository)
        {
            _alunoRepository = alunoRepository;
            _turmaRepository = turmaRepository;
        }

        public Aluno Create(string nome, DateTime dataNascimento, string responsavel, Guid turmaId, string? observacoes = null, string? fotoUrl = null, bool temUniforme = false, bool? recebeuUniforme = null)
        {
            if (_turmaRepository.GetById(turmaId) == null)
                throw new InvalidOperationException("Turma não encontrada.");
            var aluno = new Aluno(nome, dataNascimento, responsavel, turmaId, observacoes, fotoUrl, temUniforme, recebeuUniforme);
            _alunoRepository.Add(aluno);
            return aluno;
        }

        public Aluno? GetById(Guid id) => _alunoRepository.GetById(id);

        public IReadOnlyList<Aluno> GetByTurmaId(Guid turmaId) => _alunoRepository.GetByTurmaId(turmaId);

        public Aluno? Update(Guid id, string nome, DateTime dataNascimento, string responsavel, string? observacoes, string? fotoUrl, bool temUniforme, bool? recebeuUniforme)
        {
            var aluno = _alunoRepository.GetById(id);
            if (aluno == null) return null;
            aluno.Update(nome, dataNascimento, responsavel, observacoes, fotoUrl, temUniforme, recebeuUniforme);
            _alunoRepository.Update(aluno);
            return aluno;
        }

        public bool Delete(Guid id)
        {
            var aluno = _alunoRepository.GetById(id);
            if (aluno == null) return false;
            _alunoRepository.Delete(aluno);
            return true;
        }
    }
}
