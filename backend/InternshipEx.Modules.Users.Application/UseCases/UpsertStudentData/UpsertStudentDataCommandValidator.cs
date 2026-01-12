using FluentValidation;
using InternshipEx.Modules.Users.Application.UseCases.UpsertProfile;

namespace InternshipEx.Modules.Users.Application.UseCases.UpsertStudentData
{
    public class UpsertStudentDataCommandValidator : AbstractValidator<UpsertStudentDataCommand>
    {
        public UpsertStudentDataCommandValidator()
        {
            RuleFor(x => x.University).NotEmpty();
            RuleFor(x => x.Faculty).NotEmpty();
            RuleFor(x => x.Degree)
                .NotEmpty()
                .Must(d => d == "Bachelor" || d == "Master")
                .WithMessage("Degree must be either 'Bachelor' or 'Master'.");
            RuleFor(x => x.YearOfStudy).GreaterThan(0).LessThan(7);
            When(x => !string.IsNullOrEmpty(x.Specialization), () =>
            {
                RuleFor(x => x.Specialization).MaximumLength(200);
            });
        }
    }
}
