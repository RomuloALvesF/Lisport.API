using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Lisport.API.Application.DTOs.Auth;
using Lisport.API.Domain.Entities;
using Lisport.API.Domain.Enums;
using Lisport.API.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Lisport.API.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public LoginResponseDto? Login(string email, string password)
        {
            var user = _userRepository.GetByEmail(email);
            if (user == null || string.IsNullOrEmpty(user.PasswordHash))
                return null;
            if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                return null;
            return BuildTokenResponse(user);
        }

        public LoginResponseDto? Register(RegisterRequestDto request)
        {
            if (_userRepository.GetByEmail(request.Email) != null)
                return null;
            var role = ParseRole(request.Role);
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            var user = new User(request.Name, request.Email, passwordHash, role);
            _userRepository.Add(user);
            return BuildTokenResponse(user);
        }

        public LoginResponseDto? Bootstrap(RegisterRequestDto request)
        {
            if (_userRepository.Any())
                return null;
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            var user = new User(request.Name, request.Email, passwordHash, Role.Gestor);
            _userRepository.Add(user);
            return BuildTokenResponse(user);
        }

        public LoginResponseDto? DefinirSenhaInicial(string email, string novaSenha)
        {
            var user = _userRepository.GetByEmail(email);
            if (user == null) return null;
            if (!string.IsNullOrEmpty(user.PasswordHash))
                return null; // já tem senha
            user.SetPasswordHash(BCrypt.Net.BCrypt.HashPassword(novaSenha));
            _userRepository.Update(user);
            return BuildTokenResponse(user);
        }

        private static Role ParseRole(string role)
        {
            return role?.ToLowerInvariant() switch
            {
                "gestor" => Role.Gestor,
                "visualizadorexterno" or "visualizador" => Role.VisualizadorExterno,
                _ => Role.Professor
            };
        }

        private LoginResponseDto BuildTokenResponse(User user)
        {
            var expiresMinutes = _configuration.GetValue<int>("Jwt:ExpirationMinutes", 60);
            var expires = DateTime.UtcNow.AddMinutes(expiresMinutes);
            var token = GenerateJwt(user, expires);
            return new LoginResponseDto
            {
                Token = token,
                UserId = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role.ToString(),
                ExpiresAt = expires
            };
        }

        private string GenerateJwt(User user, DateTime expires)
        {
            var key = _configuration["Jwt:SecretKey"] ?? throw new InvalidOperationException("Jwt:SecretKey not configured.");
            var issuer = _configuration["Jwt:Issuer"] ?? "Lisport.API";
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim("name", user.Name)
            };
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: _configuration["Jwt:Audience"] ?? issuer,
                claims: claims,
                expires: expires,
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
