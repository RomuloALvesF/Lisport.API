using Lisport.API.Application.DTOs;
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

        public User? GetById( Guid id)
        {
            return _repository.GetById(id);

        }

        public User? Update(Guid id, string name, string email)
        {
            var user = _repository.GetById(id);

            if (user == null) 
            {
                return null;
            }

            if (!string.IsNullOrWhiteSpace(name))
            {
                user.UpdateName(name);
            }

            if (!string.IsNullOrWhiteSpace(email))
            {
                user.UpdateEmail(email);            
            }

            _repository.Update(user);

            return user;
        }

    }
}
