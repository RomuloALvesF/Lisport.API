namespace Lisport.API.Application.DTOs.Presenca
{
    public class PresencaListaItemDto
    {
        public Guid AlunoId { get; set; }
        public string AlunoNome { get; set; } = string.Empty;
        public bool Presente { get; set; }
    }
}
