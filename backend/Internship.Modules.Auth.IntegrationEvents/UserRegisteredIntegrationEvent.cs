namespace Internship.Modules.Auth.IntegrationEvents
{
    public record UserRegisteredIntegrationEvent(Guid UserId, string Role);
}
