namespace Lisport.API.Application.DTOs.Aluno
{
    public class UpdateAlunoRequestDto
    {
        public string Nome { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }
        public string Responsavel { get; set; } = string.Empty;
        public string? Observacoes { get; set; }
        public string? FotoUrl { get; set; }
        public bool TemUniforme { get; set; }
        public bool? RecebeuUniforme { get; set; }
    }
}
