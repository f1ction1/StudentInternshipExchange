using Modules.Common.Domain.Abstractions;

namespace InternshipEx.Modules.Users.Domain.Entities.Profiles;

public static class ProfileErrors
{
    public static Error NotFound => new("Profile.NotFound", "The profile was not found.");
    public static Error NotFoundOrEmpty => new("Profile.NotFoundOrEmpty", "The profile was not found or haven't created yet");
}
