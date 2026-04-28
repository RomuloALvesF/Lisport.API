using Lisport.API.Domain.Enums;

namespace Lisport.API.Domain.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
<<<<<<< HEAD
        public string? PasswordHash { get; private set; }
        public Role Role { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private User() { } // EF Core

        public User(string name, string email)
=======
        public string PasswordHash { get; private set; }
        public UserRole Role { get; private set; }
        public bool MustChangePassword { get; private set; }
        public Guid? CreatedByUserId { get; private set; }
        public ICollection<ClassGroup> Classes { get; private set; } = new List<ClassGroup>();
        public DateTime CreatedAt { get; private set; }

        private User() { }

        public User(string name, string email, string passwordHash, UserRole role, Guid? createdByUserId, bool mustChangePassword) 
>>>>>>> df4a018882a686e4ea631a2b898d710c7d421f71
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
<<<<<<< HEAD
            CreatedAt = DateTime.UtcNow;
            Role = Role.Professor;
        }
=======
            PasswordHash = passwordHash;
            Role = role;
            CreatedByUserId = createdByUserId;
            MustChangePassword = mustChangePassword;
            CreatedAt = DateTime.Now;
>>>>>>> df4a018882a686e4ea631a2b898d710c7d421f71

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

<<<<<<< HEAD
        public void SetPasswordHash(string passwordHash)
        {
            PasswordHash = passwordHash;
        }

        public void SetRole(Role role)
        {
            Role = role;
        }
=======
        public void UpdateRole(UserRole role)
        {
            Role = role;
        }

        public void UpdatePassword(string passwordHash, bool mustChangePassword)
        {
            PasswordHash = passwordHash;
            MustChangePassword = mustChangePassword;
        }
>>>>>>> df4a018882a686e4ea631a2b898d710c7d421f71
    }
}
