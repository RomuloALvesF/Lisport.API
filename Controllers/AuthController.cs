using Lisport.API.Application.DTOs.Auth;
using Lisport.API.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lisport.API.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginRequestDto request)
        {
            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
                return BadRequest("Email e senha são obrigatórios.");
            var result = _authService.Login(request.Email, request.Password);
            if (result == null)
                return Unauthorized("Credenciais inválidas.");
            return Ok(result);
        }

        [HttpPost("definir-senha-inicial")]
        [AllowAnonymous]
        public IActionResult DefinirSenhaInicial([FromBody] DefinirSenhaInicialRequestDto request)
        {
            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.NovaSenha))
                return BadRequest("Email e nova senha são obrigatórios.");
            var result = _authService.DefinirSenhaInicial(request.Email, request.NovaSenha);
            if (result == null)
                return BadRequest("Usuário não encontrado ou já possui senha definida. Use /auth/login para acessar.");
            return Ok(result);
        }

        [HttpPost("bootstrap")]
        [AllowAnonymous]
        public IActionResult Bootstrap([FromBody] RegisterRequestDto request)
        {
            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password) || string.IsNullOrWhiteSpace(request.Name))
                return BadRequest("Nome, email e senha são obrigatórios.");
            var result = _authService.Bootstrap(request);
            if (result == null)
                return BadRequest("O sistema já possui usuários cadastrados. Use /auth/login para acessar.");
            return Ok(result);
        }

        [HttpPost("register")]
        [Authorize(Roles = "Gestor")]
        public IActionResult Register([FromBody] RegisterRequestDto request)
        {
            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password) || string.IsNullOrWhiteSpace(request.Name))
                return BadRequest("Nome, email e senha são obrigatórios.");
            var result = _authService.Register(request);
            if (result == null)
                return BadRequest("Email já cadastrado.");
            return CreatedAtAction(nameof(Login), result);
        }
    }
}
