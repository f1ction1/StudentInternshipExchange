using InternshipEx.Modules.Practices.Domain.DTOs;
using MediatR;

namespace InternshipEx.Modules.Practices.Application.UseCases.Internships.AddInternship
{
    public record AddInternshipCommand(AddInternshipDto dto) : IRequest;
}
