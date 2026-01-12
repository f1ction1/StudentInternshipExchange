using MediatR;

namespace InternshipEx.Modules.Users.Application.UseCases.IsCompleteEmployerProfile
{
    public record IsCompleteEmployerProfileCommand(Guid id) : IRequest<bool>;
}
