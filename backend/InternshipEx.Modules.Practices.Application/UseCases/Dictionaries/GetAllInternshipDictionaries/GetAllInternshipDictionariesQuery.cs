using InternshipEx.Modules.Practices.Domain.DTOs.Dictionaries;
using MediatR;

namespace InternshipEx.Modules.Practices.Application.UseCases.Dictionaries.GetAllInternshipDictionaries
{
    public record GetAllInternshipDictionariesQuery() : IRequest<AllIntenshipDictionariesDto>;
}
