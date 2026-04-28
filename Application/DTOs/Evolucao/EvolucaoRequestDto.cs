namespace Lisport.API.Application.DTOs.Evolucao
{
    public class EvolucaoRequestDto
    {
        public string Periodo { get; set; } = string.Empty; // "2025-03" ou "2025-B1"
        public int EvolucaoFisica { get; set; }   // -1, 0, 1
        public int EvolucaoTecnica { get; set; }
        public int Comportamento { get; set; }
        public string? Observacao { get; set; }
    }
}
