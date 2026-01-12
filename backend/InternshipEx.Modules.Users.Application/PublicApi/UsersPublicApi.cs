using InternshipEx.Modules.Users.Application.Interfaces;
using InternshipEx.Modules.Users.Contracts;
using InternshipEx.Modules.Users.Domain.Entities.Students;
using Modules.Common.Application.Exceptions;
using Modules.Common.Domain.Abstractions;

namespace InternshipEx.Modules.Users.Application.PublicApi
{
    public class UsersPublicApi(IUnitOfWork unitOfWork) : IUsersPublicApi
    {
        public async Task<EmployerDataDto> GetEmployerDataAsync(Guid profileId)
        {
            var employer = await unitOfWork.Employers.GetByProfileIdAsync(profileId);
            if (employer == null)
                throw new NotFoundException($"Employer not found for the given profile ID - {profileId}.");

            return new EmployerDataDto(employer.Id, employer.CompanyName, null!); // setting logo to null for now, but later should be changed when implementing file storage
        }

        public async Task<Guid> GetEmployerIdByProfileId(Guid profileId)
        {
            var profile = await unitOfWork.Profiles.GetByIdAsync(profileId);
            return profile?.EmployerId ?? throw new NotFoundException($"Profile or Employer not found for the given profile ID - {profileId}.");
        }

        public async Task<Result<IList<StudentDto>>> GetStudentsByIdsAsync(IReadOnlyCollection<Guid> studentIds, CancellationToken cancellationToken)
        {
            var students = await unitOfWork.Students.GetByIdsAsync(studentIds, cancellationToken);
            if(students == null || students.Count == 0)
            {
                return Result.Failure<IList<StudentDto>>(StudentErrors.NotFound);
            }
            if(students.Count != studentIds.Count)
            {
                return Result.Failure<IList<StudentDto>>(new Error("Student.SomeOfStudentsNotFound", "Some student(s) was not found"));
            }
            if(students.Any(s => s.Profile == null))
            {
                return Result.Failure<IList<StudentDto>>(new Error("Student.ProfileIsNull", "Profile of some student(s) is null"));
            }
            return Result.Success((IList<StudentDto>)students.Select(s => new StudentDto(
                s.ProfileId,
                s.Profile.FirstName!,
                s.Profile.LastName!,
                s.Profile.PhoneNumber!,
                s.University,
                s.Specialization,
                s.YearOfStudy
            )).ToList());
        }

        public async Task<Result> IsStudentCanApplyAsync(Guid profileId, CancellationToken cancellationToken)
        {
            var profile = await unitOfWork.Profiles.GetByIdAsync(profileId);
            if (profile == null)
            {
                return Result.Failure(StudentErrors.NotFound);
            }
                
            if(!profile.IsProfileComplete("student"))
            {
                return Result.Failure(StudentErrors.IsNotComplete);
            }

            return Result.Success();
        }
    }
}
