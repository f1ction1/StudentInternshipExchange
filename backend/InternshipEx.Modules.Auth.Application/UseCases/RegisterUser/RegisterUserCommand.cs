using InternshipEx.Modules.Auth.Application.DTOs;
using MediatR;

namespace InternshipEx.Modules.Auth.Application.UseCases.RegisterUser
{
    public record RegisterUserCommand(string Email, string Password, string Role) : IRequest<AuthResponseDto>;
}
