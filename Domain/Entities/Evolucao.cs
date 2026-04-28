using Lisport.API.Domain.Enums;

namespace Lisport.API.Domain.Entities
{
    public class Evolucao
    {
        public Guid Id { get; private set; }
        public Guid AlunoId { get; private set; }
        public string Periodo { get; private set; } // ex: "2025-03" ou "2025-B1"
        public NivelEvolucao EvolucaoFisica { get; private set; }
        public NivelEvolucao EvolucaoTecnica { get; private set; }
        public NivelEvolucao Comportamento { get; private set; }
        public string? Observacao { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public virtual Aluno? Aluno { get; private set; }

        private Evolucao() { }

        public Evolucao(Guid alunoId, string periodo, NivelEvolucao evolucaoFisica, NivelEvolucao evolucaoTecnica, NivelEvolucao comportamento, string? observacao = null)
        {
            Id = Guid.NewGuid();
            AlunoId = alunoId;
            Periodo = periodo;
            EvolucaoFisica = evolucaoFisica;
            EvolucaoTecnica = evolucaoTecnica;
            Comportamento = comportamento;
            Observacao = observacao;
            CreatedAt = DateTime.UtcNow;
        }

        public void Update(NivelEvolucao evolucaoFisica, NivelEvolucao evolucaoTecnica, NivelEvolucao comportamento, string? observacao)
        {
            EvolucaoFisica = evolucaoFisica;
            EvolucaoTecnica = evolucaoTecnica;
            Comportamento = comportamento;
            Observacao = observacao;
        }
    }
}
