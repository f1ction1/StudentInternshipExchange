namespace IntershipEx.Modules.Applications.Application.UseCases.Applications.GetApplicationDetail;

public record ApplicationDetailResponse(
        Guid InternshipId,
        string InternshipTitle,
        string CompanyName,
        string CompanyLocation,
        string CoverLetter,
        string? RejectionReason,
        string status,
        IReadOnlyList<StatusDto> StatusHistory
    );
