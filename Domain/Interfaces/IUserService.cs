using Lisport.API.Domain.Entities;
using Lisport.API.Domain.Enums;
namespace Lisport.API.Domain.Interfaces
{
    public interface IUserService
    {
        User? GetById(Guid id);

        User? Update(Guid id, string? name, string? email, UserRole? role);

        bool Delete(Guid id);
    }
}
