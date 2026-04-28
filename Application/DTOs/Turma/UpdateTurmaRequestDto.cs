namespace Lisport.API.Application.DTOs.Turma
{
    public class UpdateTurmaRequestDto
    {
        public string Nome { get; set; } = string.Empty;
        public string Modalidade { get; set; } = string.Empty;
        public string FaixaEtaria { get; set; } = string.Empty;
        public string DiasHorarios { get; set; } = string.Empty;
    }
}
