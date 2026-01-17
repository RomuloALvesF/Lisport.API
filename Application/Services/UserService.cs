using Lisport.API.Domain.Entities;
using Lisport.API.Domain.Interfaces;

namespace Lisport.API.Application.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public User Create(string name, string email)
        {
            var user = new User(name, email);

            _repository.Add(user);

            return user;
        }
    }
}
