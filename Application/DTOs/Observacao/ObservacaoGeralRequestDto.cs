namespace Lisport.API.Application.DTOs.Observacao
{
    public class ObservacaoGeralRequestDto
    {
        public string Texto { get; set; } = string.Empty;
        public Guid? TurmaId { get; set; }
        public Guid? AlunoId { get; set; }
    }
}
