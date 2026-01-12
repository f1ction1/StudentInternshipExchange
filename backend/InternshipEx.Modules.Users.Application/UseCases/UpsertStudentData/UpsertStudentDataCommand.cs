using MediatR;

namespace InternshipEx.Modules.Users.Application.UseCases.UpsertStudentData
{
    public record UpsertStudentDataCommand(
        string University,
        string Faculty,
        string Degree,
        int YearOfStudy,
        string? Specialization) : IRequest;
}
