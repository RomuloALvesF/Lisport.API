namespace Lisport.API.Application.DTOs.Evolucao
{
    public class EvolucaoResponseDto
    {
        public Guid Id { get; set; }
        public Guid AlunoId { get; set; }
        public string Periodo { get; set; } = string.Empty;
        public int EvolucaoFisica { get; set; }
        public int EvolucaoTecnica { get; set; }
        public int Comportamento { get; set; }
        public string? Observacao { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
