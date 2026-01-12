using InternshipEx.Modules.Users.Application.Interfaces;
using InternshipEx.Modules.Users.Domain.Entities;
using Modules.Common.Application;
using MediatR;

namespace InternshipEx.Modules.Users.Application.UseCases.UpsertStudentData
{
    public class UpsertStudentDataCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService) : IRequestHandler<UpsertStudentDataCommand>
    {
        public async Task Handle(UpsertStudentDataCommand request, CancellationToken cancellationToken)
        {
            Guid userId = currentUserService.UserId;

            var student = await unitOfWork.Students.GetByIdAsync(userId, cancellationToken);
            if (student is not null)
            {
                student.University = request.University;
                student.Faculty = request.Faculty;
                student.Degree = request.Degree;
                student.YearOfStudy = request.YearOfStudy;
                student.Specialization = request.Specialization;    
            }
            else
            {
                student = Student.Create(userId, request.University, request.Faculty, request.Degree,
                    request.YearOfStudy, request.Specialization);
                await unitOfWork.Students.AddAsync(student, cancellationToken);
            }
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
