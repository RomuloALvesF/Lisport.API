using Lisport.API.Domain.Enums;

namespace Lisport.API.Application.DTOs
{
    public class UpdateUserRequestDto
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public UserRole? Role { get; set; }
    }
}
