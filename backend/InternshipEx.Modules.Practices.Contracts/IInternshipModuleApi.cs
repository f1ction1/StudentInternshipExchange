using InternshipEx.Modules.Practices.Contracts.DTOs;
using Modules.Common.Domain.Abstractions;

namespace InternshipEx.Modules.Practices.Contracts
{
    public interface IInternshipModuleApi
    {
        Task<Result<InternshipDTO>> GetInternship(Guid internshipId, CancellationToken cancellationToken);
    }
    
}
