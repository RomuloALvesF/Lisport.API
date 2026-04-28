namespace Lisport.API.Application.DTOs.Auth
{
    public class DefinirSenhaInicialRequestDto
    {
        public string Email { get; set; } = string.Empty;
        public string NovaSenha { get; set; } = string.Empty;
    }
}
