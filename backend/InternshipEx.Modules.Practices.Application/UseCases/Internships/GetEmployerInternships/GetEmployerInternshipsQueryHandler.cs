using InternshipEx.Modules.Practices.Application.Interfaces;
using InternshipEx.Modules.Practices.Domain.DTOs;
using InternshipEx.Modules.Practices.Domain.DTOs.Dictionaries;
using InternshipEx.Modules.Users.Contracts;
using Modules.Common.Application;
using MediatR;

namespace InternshipEx.Modules.Practices.Application.UseCases.Internships.GetEmployerInternships
{
    public class GetEmployerInternshipsQueryHandler(
        IUnitOfWork unitOfWork,
        ICurrentUserService currentUserService,
        IUsersPublicApi usersPublicApi) : IRequestHandler<GetEmployerInternshipsQuery, IEnumerable<GetEmployerInternshipsDto>>
    {
        public async Task<IEnumerable<GetEmployerInternshipsDto>> Handle(GetEmployerInternshipsQuery request, CancellationToken cancellationToken)
        {
            Guid profileId = currentUserService.UserId;
            var employerId = await usersPublicApi.GetEmployerIdByProfileId(profileId);
            var internships = await unitOfWork.Internships.GetInternshipsByEmployer(employerId);

            return internships.Select(x => new GetEmployerInternshipsDto()
            {
                Id = x.Id,
                Title = x.Title,
                IsRemote = x.IsRemote,
                City = new DictionaryDto() { Id = x.City.Id, Value = x.City.Name },
                Country = new DictionaryDto() { Id = x.Country.Id, Value = x.Country.Name },
                Industry = new DictionaryDto() { Id = x.Industry.Id, Value = x.Industry.Name },
                CreatedAt = x.CreatedAt,
                ExpiresAt = x.ExpiresAt,
                InternshipStatus = x.InternshipStatus.ToString(),
                Skills = x.Skills.Select(s => new DictionaryDto() { Id = s.Id, Value = s.Name })
            });
        }
    }
}
