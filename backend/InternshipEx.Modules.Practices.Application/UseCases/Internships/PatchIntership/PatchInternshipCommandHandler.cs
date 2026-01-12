using InternshipEx.Modules.Practices.Application.Interfaces;
using InternshipEx.Modules.Practices.Domain.Entities;
using Modules.Common.Application.Exceptions;
using MediatR;

namespace InternshipEx.Modules.Practices.Application.UseCases.Internships.PatchIntership
{
    public class PatchInternshipCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<PatchIntershipCommand>
    {
        public async Task Handle(PatchIntershipCommand request, CancellationToken cancellationToken)
        {
            var internshipDto = request.dto;
            var internship = await unitOfWork.Internships.GetByIdAsync(internshipDto.Id);
            if (internship == null)
            {
                throw new NotFoundException($"Internship with Id {internshipDto.Id} not found.");
            }

            List<Skill>? skills = null;

            if(internshipDto.SkillsIds != null)
            {
                skills = unitOfWork.Skills.GetWithCondition(s => internshipDto.SkillsIds.Contains(s.Id)).ToList();
            }
            internship.Patch(
                internshipDto.Title,
                internshipDto.Description,
                internshipDto.CityId,
                internshipDto.CountryId,
                internshipDto.IsRemote,
                internshipDto.IndustryId,
                skills,
                internshipDto.ExpiresAt);

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
