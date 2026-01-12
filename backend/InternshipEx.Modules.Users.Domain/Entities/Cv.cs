namespace InternshipEx.Modules.Users.Domain.Entities
{
    public class Cv
    {
        public Guid Id { get; set; }
        public string ContentType { get; set; } = string.Empty;
        public byte[] CvFile { get; set; } = null!;

        public Guid StudentId { get; set; }
        public virtual Student Student { get; set; } = null!;

        public static Cv Create(string contentType, byte[] cvFile, Guid studentId)
        {
            Cv cv = new Cv
            {
                Id = Guid.NewGuid(),
                StudentId = studentId,
                ContentType = contentType,
                CvFile = cvFile
            };
            return cv;
        }
    }
}
