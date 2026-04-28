<<<<<<< HEAD
using Lisport.API.Application.DTOs;
=======
>>>>>>> df4a018882a686e4ea631a2b898d710c7d421f71
using Lisport.API.Domain.Entities;
using Lisport.API.Domain.Enums;
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

        public User? GetById( Guid id)
        {
            return _repository.GetById(id);

        }

        public User? Update(Guid id, string? name, string? email, UserRole? role)
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
                var normalizedEmail = email.Trim().ToLowerInvariant();
                var existing = _repository.GetByEmail(normalizedEmail);
                if (existing != null && existing.Id != user.Id)
                {
                    throw new InvalidOperationException("Email já cadastrado.");
                }
                user.UpdateEmail(normalizedEmail);
            }

            if (role.HasValue)
            {
                user.UpdateRole(role.Value);
            }

            _repository.Update(user);

            return user;
        }

        public bool Delete(Guid id)
        {
            var user = _repository.GetById(id);
<<<<<<< HEAD
            if (user == null) return false;
=======

            if (user == null)
            {
                return false;
            }

>>>>>>> df4a018882a686e4ea631a2b898d710c7d421f71
            _repository.Delete(user);
            return true;
        }

    }
}
