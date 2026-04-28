namespace Lisport.API.Application.DTOs.Turma
{
    public class TurmaResponseDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Modalidade { get; set; } = string.Empty;
        public string FaixaEtaria { get; set; } = string.Empty;
        public string DiasHorarios { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
