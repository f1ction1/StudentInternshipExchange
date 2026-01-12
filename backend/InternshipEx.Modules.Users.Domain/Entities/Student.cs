namespace InternshipEx.Modules.Users.Domain.Entities
{
    public class Student
    {
        public Guid ProfileId { get; set; }
        public virtual Profile Profile { get; set; } = null!;
        public string University { get; set; } = string.Empty;
        public string Faculty { get; set; } = string.Empty;
        public string Degree { get; set; } = string.Empty;
        public int YearOfStudy { get; set; }
        public string? Specialization { get; set; } = string.Empty;

        public Guid CvId { get; set; }
        public virtual Cv Cv { get; set; } = null!;

        public static Student Create(Guid profileId,
            string university = "",
            string faculty = "",
            string degree = "",
            int yearOfStudy = 0, 
            string? specialization = default)
        {
            Student student = new Student
            {
                ProfileId = profileId,
                University = university!,
                Faculty = faculty!,
                Degree = degree!,
                YearOfStudy = yearOfStudy,
                Specialization = specialization
            };
            return student;
        }
    }
}
