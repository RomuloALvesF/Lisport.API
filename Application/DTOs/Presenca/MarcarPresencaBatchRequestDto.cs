namespace Lisport.API.Application.DTOs.Presenca
{
    public class MarcarPresencaBatchRequestDto
    {
        public DateTime Data { get; set; }
        public List<PresencaItemDto> Itens { get; set; } = new();
    }

    public class PresencaItemDto
    {
        public Guid AlunoId { get; set; }
        public bool Presente { get; set; }
    }
}
