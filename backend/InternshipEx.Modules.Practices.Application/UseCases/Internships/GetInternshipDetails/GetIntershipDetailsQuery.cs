using Modules.Common.Application.Messaging;

namespace InternshipEx.Modules.Practices.Application.UseCases.Internships.GetInternshipDetails;

public record GetIntershipDetailsQuery(Guid InternshipId) : IQuery<IntershipDetailResponse>;
