namespace InternshipEx.Modules.Practices.Application.UseCases.Internships.GetInternshipDetails;

public record IntershipDetailResponse(
        string Title,
        string Description,
        string Location,
        Guid EmployerId,
        string EmployerName,
        bool IsRemote,
        DateTime PublishedAt,
        IReadOnlyCollection<string> Skills
    );
