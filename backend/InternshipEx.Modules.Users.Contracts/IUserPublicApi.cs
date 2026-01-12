using Modules.Common.Domain.Abstractions;

namespace InternshipEx.Modules.Users.Contracts
{
    public interface IUsersPublicApi
    {
        Task<EmployerDataDto> GetEmployerDataAsync(Guid profileId);
        Task<Guid> GetEmployerIdByProfileId(Guid profileId);
        Task<Result> IsStudentCanApplyAsync(Guid profileId, CancellationToken cancellationToken);
        Task<Result<IList<StudentDto>>> GetStudentsByIdsAsync(IReadOnlyCollection<Guid> studentIds, CancellationToken cancellationToken);

    }
    public record EmployerDataDto(Guid EmployerId, string EmployerName, string EmployerLogoUrl);
    public record StudentDto(
        Guid StudentId,
        string FirstName,
        string LastName,
        string PhoneNumber,
        string University,
        string? Specialization,
        int YearOfStudy
        );
};
