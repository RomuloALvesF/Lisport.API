using Lisport.API.Domain.Entities;
using System.Runtime.CompilerServices;
namespace Lisport.API.Domain.Interfaces
{
    public interface IUserService
    {
        User Create(string name, string email);

        User? GetById(Guid id);

        User? Update(Guid id, string? name, string? email);

        User Delete(Guid id);
    }
}
