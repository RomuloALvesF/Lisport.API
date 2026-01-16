using Lisport.API.Domain.Entities;
namespace Lisport.API.Domain.Interfaces
{
    public interface IUserService
    {
        User Create(string name, string email);
    }
}
