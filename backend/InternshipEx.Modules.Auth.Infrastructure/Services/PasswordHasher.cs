using InternshipEx.Modules.Auth.Application.Interfaces;
using InternshipEx.Modules.Auth.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace InternshipEx.Modules.Auth.Infrastructure.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        public string Hash(string password) =>
            new PasswordHasher<User>().HashPassword(null!, password);

        public bool Verify(string password, string hashed) =>
            new PasswordHasher<User>().VerifyHashedPassword(null!, hashed, password) == PasswordVerificationResult.Success;
    }
}
