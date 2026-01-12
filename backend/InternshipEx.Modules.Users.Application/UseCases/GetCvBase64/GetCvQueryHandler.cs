using InternshipEx.Modules.Users.Application.Interfaces;
using Modules.Common.Application;
using Modules.Common.Application.Exceptions;
using MediatR;

namespace InternshipEx.Modules.Users.Application.UseCases.GetCvBase64
{
    public class GetCvQueryHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService) : IRequestHandler<GetCvQuery, string?>
    {
        public async Task<string?> Handle(GetCvQuery request, CancellationToken cancellationToken)
        {
            Guid studentId;
            if(request.studentId != null)
            {
                studentId = request.studentId.Value;
            }
            else
            {
                studentId = currentUserService.UserId;
            }
            var cv = await unitOfWork.Cvs.GetCvByStudentId(studentId);
            if (cv is null || cv.CvFile is null || cv.CvFile.Length == 0)
            {
                return null;
            }

            return Convert.ToBase64String(cv.CvFile); 
        }
    }
}
