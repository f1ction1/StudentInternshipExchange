using InternshipEx.Modules.Users.Application.Interfaces;
using InternshipEx.Modules.Users.Domain.Entities;
using Modules.Common.Application;
using Modules.Common.Application.Exceptions;
using MediatR;

namespace InternshipEx.Modules.Users.Application.UseCases.UploadCv
{
    public class UploadCvCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService) : IRequestHandler<UploadCvCommand>
    {
        public async Task Handle(UploadCvCommand request, CancellationToken cancellationToken)
        {
            var studentId = currentUserService.UserId;

            var student = await unitOfWork.Students.GetByIdAsync(studentId, cancellationToken)
                ?? throw new NotFoundException("Student not found.");

            Cv? existingCv = null;
            if (student.CvId != Guid.Empty)
            {
                existingCv = await unitOfWork.Cvs.GetByIdAsync(student.CvId, cancellationToken);
            }

            if (existingCv is null)
            {
                var cv = Cv.Create(request.ContentType, request.CvFile, studentId);
                student.CvId = cv.Id;
                await unitOfWork.Cvs.AddAsync(cv);
            } else
            {
                existingCv.ContentType = request.ContentType;
                existingCv.CvFile = request.CvFile;
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
