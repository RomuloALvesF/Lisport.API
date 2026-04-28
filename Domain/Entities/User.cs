using Lisport.API.Domain.Enums;

namespace Lisport.API.Domain.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string? PasswordHash { get; private set; }
        public Role Role { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private User() { } // EF Core

        public User(string name, string email)
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            CreatedAt = DateTime.UtcNow;
            Role = Role.Professor;
        }

        public User(string name, string email, string passwordHash, Role role)
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            Role = role;
            CreatedAt = DateTime.UtcNow;
        }

        public void UpdateName(string name)
        {
            Name = name;
        }

        public void UpdateEmail(string email)
        {
            Email = email;
        }

        public void SetPasswordHash(string passwordHash)
        {
            PasswordHash = passwordHash;
        }

        public void SetRole(Role role)
        {
            Role = role;
        }
    }
}
