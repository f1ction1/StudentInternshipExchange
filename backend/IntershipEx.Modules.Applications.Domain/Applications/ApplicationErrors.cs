using Modules.Common.Domain.Abstractions;

namespace IntershipEx.Modules.Applications.Domain.Applications;

public static class ApplicationErrors 
{
    public static readonly Error NotFound = new(
        "Application.NotFound",
        "The application(s) was not found."
    );

    public static readonly Error AlreadyApplied = new(
        "Application.AlreadyApplied",
        "You have already applied for this internship");

    public static readonly Error AlreadyReviewed = new(
        "Application.AlreadyReviewed",
        "You have already reviewed this application");

    public static readonly Error AlreadyRejected = new(
        "Application.AlreadyRejected",
        "You have already rejected this application");
}
