using System;

namespace Lisport.API.Application.DTOs
{
    public class LoginResponseDto
    {
        public string AccessToken { get; set; }
        public DateTime ExpiresAt { get; set; }
        public bool MustChangePassword { get; set; }
        public UserResponseDto User { get; set; }
    }
}
