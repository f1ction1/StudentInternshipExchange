using InternshipEx.Modules.Practices.Domain.DTOs;
using MediatR;

namespace InternshipEx.Modules.Practices.Application.UseCases.Internships.GetEmployerInternship
{
    public record GetEmployerInternshipQuery(Guid internshipId) : IRequest<GetEmployerInternshipDto>;
}
