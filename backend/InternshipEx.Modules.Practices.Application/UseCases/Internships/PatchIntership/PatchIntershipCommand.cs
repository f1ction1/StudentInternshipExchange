using InternshipEx.Modules.Practices.Domain.DTOs;
using MediatR;

namespace InternshipEx.Modules.Practices.Application.UseCases.Internships.PatchIntership
{
    public record PatchIntershipCommand(PatchInternshipDto dto) : IRequest;
}
