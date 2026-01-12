using InternshipEx.Modules.Users.Domain.DTOs;
using MediatR;

namespace InternshipEx.Modules.Users.Application.UseCases.EditEmployer
{
    public record EditEmployerCommand(EmployerDto Employer) : IRequest;
}
