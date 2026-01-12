using InternshipEx.Modules.Practices.Domain.DTOs;
using MediatR;

namespace InternshipEx.Modules.Practices.Application.UseCases.Internships.GetEmployerInternships
{
    public record GetEmployerInternshipsQuery() : IRequest<IEnumerable<GetEmployerInternshipsDto>>;
}
