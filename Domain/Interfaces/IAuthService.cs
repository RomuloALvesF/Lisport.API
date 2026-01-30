using Lisport.API.Application.DTOs;
using Lisport.API.Domain.Entities;
using Lisport.API.Domain.Enums;

namespace Lisport.API.Domain.Interfaces
{
    public interface IAuthService
    {
        User Register(string name, string email, string password, UserRole role, Guid? createdByUserId, bool mustChangePassword);
        LoginResponseDto Login(string email, string password);
        void ChangePassword(Guid userId, string currentPassword, string newPassword);
    }
}
