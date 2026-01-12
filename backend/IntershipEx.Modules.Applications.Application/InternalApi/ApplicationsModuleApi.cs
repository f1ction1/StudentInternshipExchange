using InternshipEx.Modules.Applications.Contracts;
using InternshipEx.Modules.Applications.Contracts.Contracts;
using IntershipEx.Modules.Applications.Application.Abstractions.Data;
using Modules.Common.Domain.Abstractions;

namespace IntershipEx.Modules.Applications.Application.InternalApi
{
    public class ApplicationsModuleApi(
        IUnitOfWork unitOfWork) : IApplicationsModuleApi
    {
        public async Task<Result<ApplicationsByInternshipResponse>> GetApplicationsByInternshipAsync(Guid internshipId)
        {
            var applications = await unitOfWork.Applications.GetApplicatiosByInternshipAsync(internshipId);
            if(applications is null || applications.Count == 0)
            {
                return Result.Failure<ApplicationsByInternshipResponse>(new Error("Applications.NotFound", "Applications for given internship id was not found"));
            }
            ApplicationsByInternshipResponse response = new();
            if (applications.Count > 0) 
            {
                response.InternshipInfo = new InternshipInfoDto()
                {
                    Id = applications[0].IntershipId,
                    Title = applications[0].InternshipInfo.Title,
                    CompanyName = applications[0].InternshipInfo.CompanyName,
                    CompanyLocation = applications[0].InternshipInfo.CompanyLocation
                };
            }
            response.Applications = applications.Select(a => new ApplicationDto()
            {
                UserId = a.StudentId,
                ApplicationId = a.Id,
                CreatedAt = a.CreatedAt,
                Status = (int)a.Status,
                CoverLetter = a.CoverLetter,
                ReviewedAt = a.Review?.ReviewedAt,
                ReviewNotes = a.Review?.ReviewNotes
            }).ToList();
            return response;
        }
    }
}
