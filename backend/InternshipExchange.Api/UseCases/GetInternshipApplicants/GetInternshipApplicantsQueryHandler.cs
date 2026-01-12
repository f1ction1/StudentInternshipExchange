using InternshipEx.Modules.Applications.Contracts;
using InternshipEx.Modules.Users.Contracts;
using Modules.Common.Application.Messaging;
using Modules.Common.Domain.Abstractions;

namespace InternshipExchange.Api.UseCases.GetInternshipApplicants
{
    public class GetInternshipApplicantsQueryHandler(
        IApplicationsModuleApi applicationsModule,
        IUsersPublicApi usersModule) : IQueryHandler<GetInternshipApplicantsQuery, ApplicantsResponse>
    {
        public async Task<Result<ApplicantsResponse>> Handle(GetInternshipApplicantsQuery request, CancellationToken cancellationToken)
        {
            var applications = await applicationsModule.GetApplicationsByInternshipAsync(request.internshipId);
            if (applications.IsFailure)
            {
                return Result.Failure<ApplicantsResponse>(applications.Error);
            }
            var students = await usersModule.GetStudentsByIdsAsync(applications.Value.Applications.Select(a => a.UserId).ToList(), cancellationToken);
            if(students.IsFailure)
            {
                return Result.Failure<ApplicantsResponse>(students.Error);
            }
            var response = new ApplicantsResponse
            {
                InternshipInfo = applications.Value.InternshipInfo,
                Applications = applications.Value.Applications.Select(app => new ApplicationWithStudentDataDto
                {
                    UserId = app.UserId,
                    ApplicationId = app.ApplicationId,
                    CreatedAt = app.CreatedAt,
                    Status = app.Status,
                    CoverLetter = app.CoverLetter,
                    ReviewedAt = app.ReviewedAt,
                    ReviewNotes = app.ReviewNotes,
                    FirstName = students.Value.First(s => s.StudentId == app.UserId).FirstName,
                    LastName = students.Value.First(s => s.StudentId == app.UserId).LastName,
                    PhoneNumber = students.Value.First(s => s.StudentId == app.UserId).PhoneNumber,
                    University = students.Value.First(s => s.StudentId == app.UserId).University,
                    Specialization = students.Value.First(s => s.StudentId == app.UserId).Specialization,
                    YearOfStudy = students.Value.First(s => s.StudentId == app.UserId).YearOfStudy
                }).ToList()
            };
            return response;
        }
    }
}
