namespace Modules.Common.Application;

public interface ICurrentUserService
{
    Guid UserId { get; }
    string Role { get; }
}
