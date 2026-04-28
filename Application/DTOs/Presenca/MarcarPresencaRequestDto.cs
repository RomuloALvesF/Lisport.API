namespace Lisport.API.Application.DTOs.Presenca
{
    public class MarcarPresencaRequestDto
    {
        public DateTime Data { get; set; }
        public Guid AlunoId { get; set; }
        public bool Presente { get; set; }
    }
}
