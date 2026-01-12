using InternshipEx.Modules.Users.Application.Interfaces;
using InternshipEx.Modules.Users.Domain.DTOs;
using InternshipEx.Modules.Users.Domain.Entities.Students;
using Modules.Common.Application;
using Modules.Common.Application.Exceptions;
using MediatR;
using Modules.Common.Application.Messaging;
using Modules.Common.Domain.Abstractions;

namespace InternshipEx.Modules.Users.Application.UseCases.GetStudent
{
    public class GetStudentQueryHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService) : IQueryHandler<GetStudentQuery, StudentDto>
    {
        public async Task<Result<StudentDto>> Handle(GetStudentQuery request, CancellationToken cancellationToken)
        {
            var id = currentUserService.UserId;

            var student = await unitOfWork.Students.GetByIdAsync(id, cancellationToken);
            if (student is null)
                return Result.Failure<StudentDto>(StudentErrors.NotFoundOrEmpty);

            return new StudentDto
            {
                University = student.University,
                Faculty = student.Faculty,
                Degree = student.Degree,
                YearOfStudy = student.YearOfStudy,
                Specialization = student.Specialization,
            };
        }
    }
}
