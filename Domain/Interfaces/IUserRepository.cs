using Lisport.API.Domain.Entities;
namespace Lisport.API.Domain.Interfaces
{
    public interface IUserRepository
    {
        void Add(User user);
        User? GetById(Guid id);
    }
}
