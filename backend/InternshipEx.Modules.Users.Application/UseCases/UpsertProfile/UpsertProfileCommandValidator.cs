using FluentValidation;

namespace InternshipEx.Modules.Users.Application.UseCases.UpsertProfile
{
    public class UpsertProfileCommandValidator : AbstractValidator<UpsertProfileCommand>
    {
        public UpsertProfileCommandValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.PhoneNumber).MaximumLength(20);
        }
    }
}
