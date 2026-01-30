using Lisport.API.Application.DTOs;
using Lisport.API.Application.Settings;
using Lisport.API.Domain.Entities;
using Lisport.API.Domain.Enums;
using Lisport.API.Domain.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Lisport.API.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtSettings _jwtSettings;

        public AuthService(IUserRepository userRepository, IOptions<JwtSettings> jwtOptions)
        {
            _userRepository = userRepository;
            _jwtSettings = jwtOptions.Value;
        }

        public User Register(string name, string email, string password, UserRole role, Guid? createdByUserId, bool mustChangePassword)
        {
            var normalizedEmail = email.Trim().ToLowerInvariant();
            var existing = _userRepository.GetByEmail(normalizedEmail);
            if (existing != null)
            {
                throw new InvalidOperationException("Email já cadastrado.");
            }

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
            var user = new User(name, normalizedEmail, passwordHash, role, createdByUserId, mustChangePassword);

            _userRepository.Add(user);

            return user;
        }

        public LoginResponseDto Login(string email, string password)
        {
            var normalizedEmail = email.Trim().ToLowerInvariant();
            var user = _userRepository.GetByEmail(normalizedEmail);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                throw new UnauthorizedAccessException();
            }

            var (token, expiresAt) = GenerateToken(user);

            return new LoginResponseDto
            {
                AccessToken = token,
                ExpiresAt = expiresAt,
                MustChangePassword = user.MustChangePassword,
                User = new UserResponseDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Role = user.Role,
                    CreatedAt = user.CreatedAt
                }
            };
        }

        public void ChangePassword(Guid userId, string currentPassword, string newPassword)
        {
            var user = _userRepository.GetById(userId);
            if (user == null)
            {
                throw new UnauthorizedAccessException();
            }

            if (!BCrypt.Net.BCrypt.Verify(currentPassword, user.PasswordHash))
            {
                throw new UnauthorizedAccessException();
            }

            var newHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            user.UpdatePassword(newHash, mustChangePassword: false);
            _userRepository.Update(user);
        }

        private (string token, DateTime expiresAt) GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiresInMinutes);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: expires,
                signingCredentials: creds);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return (tokenString, expires);
        }
    }
}
