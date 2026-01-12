using InternshipEx.Modules.Practices.Application.Interfaces;
using InternshipEx.Modules.Practices.Domain.Entities;
using InternshipEx.Modules.Users.Contracts;
using Modules.Common.Application;
using MediatR;

namespace InternshipEx.Modules.Practices.Application.UseCases.Internships.AddInternship
{
    public class AddInternshipCommandHandler(
        IUnitOfWork unitOfWork,
        ICurrentUserService currentUserService,
        IUsersPublicApi usersPublicApi) : IRequestHandler<AddInternshipCommand>
    {
        public async Task Handle(AddInternshipCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = currentUserService.UserId;
            var internshipDto = request.dto;

            var skills = unitOfWork.Skills.GetWithCondition(s => internshipDto.SkillsIds.Contains(s.Id));
            var employerData = await usersPublicApi.GetEmployerDataAsync(currentUserId);

            var internship = Internship.Create(
                employerData.EmployerId,
                internshipDto.Title,
                internshipDto.Description,
                internshipDto.CityId,
                internshipDto.CountryId,
                internshipDto.IsRemote,
                internshipDto.IndustryId,
                skills,
                internshipDto.ExpiresAt,
                employerData.EmployerName,
                employerData.EmployerLogoUrl
            );

            await unitOfWork.Internships.Add(internship);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
