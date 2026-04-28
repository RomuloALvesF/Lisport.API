using Lisport.API.Domain.Entities;
using Lisport.API.Domain.Interfaces;

namespace Lisport.API.Application.Services
{
    public class PresencaService : IPresencaService
    {
        private readonly IPresencaRepository _presencaRepository;
        private readonly IAlunoRepository _alunoRepository;
        private readonly ITurmaRepository _turmaRepository;

        public PresencaService(IPresencaRepository presencaRepository, IAlunoRepository alunoRepository, ITurmaRepository turmaRepository)
        {
            _presencaRepository = presencaRepository;
            _alunoRepository = alunoRepository;
            _turmaRepository = turmaRepository;
        }

        public IReadOnlyList<PresencaListaItem> GetPresencaLista(Guid turmaId, DateTime data)
        {
            if (_turmaRepository.GetById(turmaId) == null) return Array.Empty<PresencaListaItem>();
            var alunos = _alunoRepository.GetByTurmaId(turmaId);
            var dataOnly = data.Date;
            var presencas = _presencaRepository.GetByTurmaAndData(turmaId, dataOnly);
            var dict = presencas.ToDictionary(p => p.AlunoId, p => p.Presente);
            return alunos.Select(a => new PresencaListaItem
            {
                AlunoId = a.Id,
                AlunoNome = a.Nome,
                Presente = dict.TryGetValue(a.Id, out var p) && p
            }).ToList();
        }

        public void MarcarPresenca(Guid turmaId, DateTime data, Guid alunoId, bool presente)
        {
            ValidarAlunoNaTurma(turmaId, alunoId);
            var dataOnly = data.Date;
            var existing = _presencaRepository.GetByTurmaDataAluno(turmaId, dataOnly, alunoId);
            if (existing != null)
            {
                existing.SetPresente(presente);
                _presencaRepository.Update(existing);
            }
            else
            {
                _presencaRepository.Add(new Presenca(turmaId, dataOnly, alunoId, presente));
            }
            _presencaRepository.SaveChanges();
        }

        public void MarcarPresencaBatch(Guid turmaId, DateTime data, IReadOnlyList<(Guid AlunoId, bool Presente)> itens)
        {
            var dataOnly = data.Date;
            foreach (var (alunoId, presente) in itens)
                ValidarAlunoNaTurma(turmaId, alunoId);
            foreach (var (alunoId, presente) in itens)
            {
                var existing = _presencaRepository.GetByTurmaDataAluno(turmaId, dataOnly, alunoId);
                if (existing != null)
                {
                    existing.SetPresente(presente);
                    _presencaRepository.Update(existing);
                }
                else
                {
                    _presencaRepository.Add(new Presenca(turmaId, dataOnly, alunoId, presente));
                }
            }
            _presencaRepository.SaveChanges();
        }

        private void ValidarAlunoNaTurma(Guid turmaId, Guid alunoId)
        {
            var aluno = _alunoRepository.GetById(alunoId);
            if (aluno == null || aluno.TurmaId != turmaId)
                throw new InvalidOperationException("Aluno não pertence à turma.");
        }
    }
}
