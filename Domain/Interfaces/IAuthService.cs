using Lisport.API.Application.DTOs.Auth;

namespace Lisport.API.Domain.Interfaces
{
    public interface IAuthService
    {
        LoginResponseDto? Login(string email, string password);
        LoginResponseDto? Register(RegisterRequestDto request);
        LoginResponseDto? Bootstrap(RegisterRequestDto request);
        LoginResponseDto? DefinirSenhaInicial(string email, string novaSenha);
    }
}
