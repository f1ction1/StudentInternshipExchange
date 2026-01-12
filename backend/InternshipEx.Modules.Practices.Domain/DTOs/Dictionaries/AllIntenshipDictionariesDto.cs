namespace InternshipEx.Modules.Practices.Domain.DTOs.Dictionaries
{
    public class AllIntenshipDictionariesDto
    {
        public List<DictionaryDto> Skills { get; set; } = new List<DictionaryDto>();
        public List<DictionaryDto> Industries { get; set; } = new List<DictionaryDto>();
        public List<DictionaryDto> Countries { get; set; } = new List<DictionaryDto>();
        public List<CityDictionaryDto> Cities { get; set; } = new List<CityDictionaryDto>();
        
    }
}
