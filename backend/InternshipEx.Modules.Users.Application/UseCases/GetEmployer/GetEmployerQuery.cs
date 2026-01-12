using InternshipEx.Modules.Users.Domain.DTOs;
using MediatR;

namespace InternshipEx.Modules.Users.Application.UseCases.GetEmployer
{
    public class GetEmployerQuery() : IRequest<EmployerDto>;
}
