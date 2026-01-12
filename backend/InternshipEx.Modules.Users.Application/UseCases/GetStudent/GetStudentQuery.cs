using InternshipEx.Modules.Users.Domain.DTOs;
using Modules.Common.Application.Messaging;

namespace InternshipEx.Modules.Users.Application.UseCases.GetStudent
{
    public record GetStudentQuery() : IQuery<StudentDto>;
}
