using MediatR;

namespace InternshipEx.Modules.Users.Application.UseCases.UpsertProfile
{
    public record UpsertProfileCommand(
        string? FirstName,
        string? LastName,
        string? PhoneNumber) : IRequest;
}
