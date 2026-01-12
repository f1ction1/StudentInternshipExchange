namespace InternshipEx.Modules.Users.Domain.Entities
{
    public class CompanySize
    {
        public int Id { get; set; }
        public string Label { get; set; } = string.Empty;
        public int? MinEmployees { get; set; } = null;
        public int? MaxEmployees { get; set; } = null;
    }
}
