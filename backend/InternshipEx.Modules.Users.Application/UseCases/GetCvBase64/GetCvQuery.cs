using MediatR;

namespace InternshipEx.Modules.Users.Application.UseCases.GetCvBase64
{
    public record GetCvQuery(Guid? studentId = null) : IRequest<string?>;
}
