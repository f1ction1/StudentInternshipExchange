using InternshipEx.Modules.Applications.Contracts.Contracts;
using Modules.Common.Domain.Abstractions;

namespace InternshipEx.Modules.Applications.Contracts
{
    public interface IApplicationsModuleApi
    {
        Task<Result<ApplicationsByInternshipResponse>> GetApplicationsByInternshipAsync(Guid internshipId);
    }
}
