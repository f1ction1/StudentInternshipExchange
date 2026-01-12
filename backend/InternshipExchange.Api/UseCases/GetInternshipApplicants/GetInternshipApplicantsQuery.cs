using Modules.Common.Application.Messaging;

namespace InternshipExchange.Api.UseCases.GetInternshipApplicants
{
    public record GetInternshipApplicantsQuery(Guid internshipId) : IQuery<ApplicantsResponse>;
}
