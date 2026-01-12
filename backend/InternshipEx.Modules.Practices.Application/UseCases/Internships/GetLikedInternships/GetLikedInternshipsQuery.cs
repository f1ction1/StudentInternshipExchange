using Modules.Common.Application.Messaging;

namespace InternshipEx.Modules.Practices.Application.UseCases.Internships.GetLikedInternships
{
    public record GetLikedInternshipsQuery() : IQuery<IList<LikedInternship>>;
}
