using InternshipEx.Modules.Auth.Domain.DomainEvents;
using InternshipEx.Modules.Auth.Domain.Primitives;

namespace InternshipEx.Modules.Auth.Domain.Entities
{
    public class User : AggregateRoot
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;

        public static User Create(string email, string passwordHash, string Role)
        {
            User user = new User
            {
                Id = Guid.NewGuid(),
                Email = email,
                PasswordHash = passwordHash,
                Role = Role
            };

            user.RaiseDomainEvent(new UserRegisteredDomainEvent(user.Id, user.Role));

            return user;
        } 
    }
}
