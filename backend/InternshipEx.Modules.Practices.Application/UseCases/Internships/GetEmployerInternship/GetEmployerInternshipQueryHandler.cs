using InternshipEx.Modules.Practices.Application.Interfaces;
using InternshipEx.Modules.Practices.Domain.DTOs;
using Modules.Common.Application.Exceptions;
using MediatR;

namespace InternshipEx.Modules.Practices.Application.UseCases.Internships.GetEmployerInternship
{
    public class GetEmployerInternshipQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetEmployerInternshipQuery, GetEmployerInternshipDto>
    {
        public async Task<GetEmployerInternshipDto> Handle(GetEmployerInternshipQuery request, CancellationToken cancellationToken)
        {
            var internshipId = request.internshipId;
            var internship = await unitOfWork.Internships.GetByIdAsync(internshipId);
            if(internship == null)
            {
                throw new NotFoundException("Internship not found");
            }
            return new GetEmployerInternshipDto
            {
                Id = internship.Id,
                Title = internship.Title,
                Description = internship.Description,
                IsRemote = internship.IsRemote,
                CountryId = internship.CountryId,
                CityId = internship.CityId,
                IndustryId = internship.IndustryId,
                ExpiresAt = internship.ExpiresAt,
                SkillsIds = internship.Skills.Select(s => s.Id).ToList()
            };
        }
    }
}
