using Modules.Common.Domain.Abstractions;

namespace InternshipEx.Modules.Users.Domain.Entities.Students;

public static class StudentErrors
{
    public static Error NotFoundOrEmpty => new("Student.NotFoundOrEmpty", "The student was not found or fields are empty.");
    public static Error NotFound => new("Student.NotFound", "The student was not found.");
    public static Error IsNotComplete => new("Student.IsNotComplete", "The student profile is not complete.");
}
