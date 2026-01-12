using MediatR;

namespace InternshipEx.Modules.Users.Application.UseCases.UploadCv
{
    public record UploadCvCommand(string ContentType, byte[] CvFile) : IRequest;
}
