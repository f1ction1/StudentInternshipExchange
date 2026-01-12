using IntershipEx.Modules.Applications.Application.Abstractions.Messaging;

namespace IntershipEx.Modules.Applications.Application.UseCases.Applications.GetApplicationDetail;

public record GetApplicationDetailQuery(Guid Id) : IQuery<ApplicationDetailResponse>;
