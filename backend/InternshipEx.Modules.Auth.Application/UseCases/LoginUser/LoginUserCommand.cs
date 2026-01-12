using InternshipEx.Modules.Auth.Application.DTOs;
using MediatR;

namespace InternshipEx.Modules.Auth.Application.UseCases.LoginUser
{
    public record LoginUserCommand(string Email, string Password) : IRequest<AuthResponseDto>;
}
