using InternshipEx.Modules.Practices.Application.Interfaces;
using InternshipEx.Modules.Practices.Domain.DTOs.Dictionaries;
using MediatR;

namespace InternshipEx.Modules.Practices.Application.UseCases.Dictionaries.GetAllInternshipDictionaries
{
    public class GetAllInternshipDictionariesQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllInternshipDictionariesQuery, AllIntenshipDictionariesDto>
    {
        public async Task<AllIntenshipDictionariesDto> Handle(GetAllInternshipDictionariesQuery request, CancellationToken cancellationToken)
        {
            var skills = await unitOfWork.Skills.GetAllAsync();
            var industries = await unitOfWork.Industries.GetAllAsync();
            var cities = await unitOfWork.Cities.GetAllAsync();
            var countries = await unitOfWork.Countries.GetAllAsync();

            return new AllIntenshipDictionariesDto
            {
                Skills = skills.Select(s => new DictionaryDto { Id = s.Id, Value = s.Name }).ToList(),
                Industries = industries.Select(i => new DictionaryDto { Id = i.Id, Value = i.Name }).ToList(),
                Countries = countries.Select(c => new DictionaryDto { Id = c.Id, Value = c.Name }).ToList(),
                Cities = cities.Select(c => new CityDictionaryDto { Id = c.Id, Value = c.Name, CountryId = c.CountryId }).ToList()
            };
        }
    }
}
