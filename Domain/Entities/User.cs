using Lisport.API.Domain.Enums;

namespace Lisport.API.Domain.Entities
{
    public class User
    {
        public Guid Id { get;  private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public UserRole Role { get; private set; }
        public bool MustChangePassword { get; private set; }
        public Guid? CreatedByUserId { get; private set; }
        public ICollection<ClassGroup> Classes { get; private set; } = new List<ClassGroup>();
        public DateTime CreatedAt { get; private set; }

        private User() { }

        public User(string name, string email, string passwordHash, UserRole role, Guid? createdByUserId, bool mustChangePassword) 
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            Role = role;
            CreatedByUserId = createdByUserId;
            MustChangePassword = mustChangePassword;
            CreatedAt = DateTime.Now;

        }

        public void UpdateName(string name)
        {
            Name = name;
        }

        public void UpdateEmail(string email)
        {
            Email = email;
        }

        public void UpdateRole(UserRole role)
        {
            Role = role;
        }

        public void UpdatePassword(string passwordHash, bool mustChangePassword)
        {
            PasswordHash = passwordHash;
            MustChangePassword = mustChangePassword;
        }
    }
}
