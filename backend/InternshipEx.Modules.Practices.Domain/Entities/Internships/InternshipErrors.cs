using Modules.Common.Domain.Abstractions;

namespace InternshipEx.Modules.Practices.Domain.Entities.Internships;

public static class InternshipErrors
{
    public static Error NotFound = new("Internship.NotFound", "The internship was not found");
    public static Error NotAcceptingApplications = new("Internship.NotAcceptingApplications", "The internship no longer acception applications");
    public static Error Expired = new("Internship.Expired", "The internship doesn't published");
}
