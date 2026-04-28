using Lisport.API.Domain.Entities;
namespace Lisport.API.Domain.Interfaces
{
    public interface IUserRepository
    {
        void Add(User user);
        User? GetById(Guid id);
        User? GetByEmail(string email);
        void Update(User user);

        void Delete(User user);
        User? GetByEmail(string email);
        bool Any();
    }
}
