using Lisport.API.Domain.Entities;
using Lisport.API.Domain.Interfaces;

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
    }
}
