using Modules.Common.Application;
using Modules.Common.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Modules.Common.Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public Guid UserId
        {
            get
            {
                var userIdClaim = _httpContextAccessor.HttpContext?.User?
                    .FindFirst(ClaimTypes.NameIdentifier)?.Value;

                return Guid.TryParse(userIdClaim, out var userId) ? userId : Guid.Empty;
            }
        }
        public string Role
                    {
            get
            {
                var roleClaim = _httpContextAccessor.HttpContext?.User?
                    .FindFirst(ClaimTypes.Role)?.Value;
                return roleClaim ?? throw new UnauthorizedException("User role is not found");
            }
        }
    }
}
