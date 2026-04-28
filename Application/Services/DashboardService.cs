using Lisport.API.Domain.Enums;
using Lisport.API.Domain.Interfaces;

namespace Lisport.API.Application.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly ITurmaRepository _turmaRepository;
        private readonly IAlunoRepository _alunoRepository;
        private readonly IPresencaRepository _presencaRepository;
        private readonly IEvolucaoRepository _evolucaoRepository;

        public DashboardService(ITurmaRepository turmaRepository, IAlunoRepository alunoRepository, IPresencaRepository presencaRepository, IEvolucaoRepository evolucaoRepository)
        {
            _turmaRepository = turmaRepository;
            _alunoRepository = alunoRepository;
            _presencaRepository = presencaRepository;
            _evolucaoRepository = evolucaoRepository;
        }

        public DashboardImpactoDto GetImpacto(DateTime de, DateTime ate)
        {
            var di = de.Date;
            var df = ate.Date;
            var turmas = _turmaRepository.GetAll();
            var totalAlunos = turmas.Sum(t => _alunoRepository.GetByTurmaId(t.Id).Count);
            var totalTurmas = turmas.Count;
            var (totalRegistros, totalPresentes) = _presencaRepository.GetStatsByPeriodo(di, df);
            var frequencia = totalRegistros > 0 ? 100.0 * totalPresentes / totalRegistros : 0;

            var ultimoPeriodo = _evolucaoRepository.GetUltimoPeriodo();
            EvolucaoGeralDto? evolucaoGeral = null;
            if (!string.IsNullOrEmpty(ultimoPeriodo))
            {
                var evs = _evolucaoRepository.GetByPeriodo(ultimoPeriodo);
                evolucaoGeral = new EvolucaoGeralDto
                {
                    MelhorouFisica = evs.Count(e => e.EvolucaoFisica == NivelEvolucao.Melhorou),
                    ManteveFisica = evs.Count(e => e.EvolucaoFisica == NivelEvolucao.Manteve),
                    ReduziuFisica = evs.Count(e => e.EvolucaoFisica == NivelEvolucao.Reduziu),
                    MelhorouTecnica = evs.Count(e => e.EvolucaoTecnica == NivelEvolucao.Melhorou),
                    ManteveTecnica = evs.Count(e => e.EvolucaoTecnica == NivelEvolucao.Manteve),
                    ReduziuTecnica = evs.Count(e => e.EvolucaoTecnica == NivelEvolucao.Reduziu),
                    MelhorouComportamento = evs.Count(e => e.Comportamento == NivelEvolucao.Melhorou),
                    ManteveComportamento = evs.Count(e => e.Comportamento == NivelEvolucao.Manteve),
                    ReduziuComportamento = evs.Count(e => e.Comportamento == NivelEvolucao.Reduziu)
                };
            }

            var dias = (df - di).Days + 1;
            var periodoAnteriorDe = di.AddDays(-dias);
            var periodoAnteriorAte = di.AddDays(-1);
            var (regAnt, presAnt) = _presencaRepository.GetStatsByPeriodo(periodoAnteriorDe, periodoAnteriorAte);
            var freqAnterior = regAnt > 0 ? 100.0 * presAnt / regAnt : 0;

            return new DashboardImpactoDto
            {
                TotalAlunosAtivos = totalAlunos,
                TotalTurmas = totalTurmas,
                FrequenciaMediaPercentual = Math.Round(frequencia, 1),
                EvolucaoGeral = evolucaoGeral,
                ComparativoPeriodo = new FrequenciaComparativoDto
                {
                    FrequenciaPeriodoAtual = Math.Round(frequencia, 1),
                    FrequenciaPeriodoAnterior = Math.Round(freqAnterior, 1)
                }
            };
        }
    }
}
