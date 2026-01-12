namespace InternshipEx.Modules.Applications.Contracts.Contracts
{
    public class ApplicationDto
    {
        public Guid UserId { get; set; }
        public Guid ApplicationId { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Status { get; set; }
        public string? CoverLetter { get; set; }
        public DateTime? ReviewedAt { get; set; }
        public string? ReviewNotes { get; set; }
    }
}
