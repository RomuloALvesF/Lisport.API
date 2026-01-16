using Lisport.API.Domain.Entities;
using Lisport.API.Domain.Interfaces;

namespace Lisport.API.Application.Services
{
    public class UserService : IUserService
    {
        public User Create(string name, string email)
        {
            return new User(name, email);
        }
    }
}
