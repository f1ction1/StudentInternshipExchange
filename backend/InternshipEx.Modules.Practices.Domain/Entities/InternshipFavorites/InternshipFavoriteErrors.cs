using Modules.Common.Domain.Abstractions;

namespace InternshipEx.Modules.Practices.Domain.Entities.InternshipFavorites
{
    public static class InternshipFavoriteErrors
    {
        public static readonly Error NotFound = new(
            "InternshipFavorite.NotFound",
            "The internship favorite was not found."
        );
    }
}
