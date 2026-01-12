using InternshipEx.Modules.Auth.Domain.Entities;

namespace InternshipEx.Modules.Auth.Application.Interfaces
{
    public interface IJwtTokenService
    {
        string CreateToken(User user);
    }
}
