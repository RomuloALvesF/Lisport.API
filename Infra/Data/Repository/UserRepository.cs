using Lisport.API.Domain.Entities;
using Lisport.API.Domain.Interfaces;
using System.Security.Cryptography.X509Certificates;

namespace Lisport.API.Infra.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context) 
        { 
            _context = context;
        }

        public void Add(User user)
        { 
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public User? GetById(Guid id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);

        }

        public void Update(User user) 
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void Delete(User user) 
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

    }
}
