using MediatR;

namespace InternshipEx.Modules.Users.Application.UseCases.CompleteEmployerProfile
{
    public record UpsertEmployerProfileCommand(
        string FirstName,
        string LastName,
        string PhoneNumber,
        string CompanyName,
        int CompanySizeId,
        string? CompanyDescription,
        string? CompanyWebsite) : IRequest;
}
