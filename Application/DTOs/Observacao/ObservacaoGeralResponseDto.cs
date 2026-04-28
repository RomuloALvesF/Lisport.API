namespace Lisport.API.Application.DTOs.Observacao
{
    public class ObservacaoGeralResponseDto
    {
        public Guid Id { get; set; }
        public Guid? TurmaId { get; set; }
        public Guid? AlunoId { get; set; }
        public string Texto { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
