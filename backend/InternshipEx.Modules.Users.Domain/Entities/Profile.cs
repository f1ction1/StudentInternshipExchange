namespace InternshipEx.Modules.Users.Domain.Entities
{
    public class Profile
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; } = string.Empty;

        public Guid? EmployerId { get; set; } = null;
        public virtual Employer? Employer { get; set; } = null;
        public virtual Student? Student { get; set; } = null;

        public static Profile Create(Guid id, string? firstName = default, string? lastName = default, string? phoneNumber = default)
        {
            Profile profile = new Profile
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                PhoneNumber = phoneNumber
            };
            return profile;
        }

        //Check if required fields are filled
        public bool IsProfileComplete(string role)
        {
            if(role == "employer")
            {
                if (Employer != null)
                {
                    return !string.IsNullOrEmpty(FirstName) &&
                           !string.IsNullOrEmpty(LastName) &&
                           !string.IsNullOrEmpty(PhoneNumber) &&
                           !string.IsNullOrEmpty(Employer.CompanyName) &&
                           Employer.CompanySizeId > 0;
                }
                return false;
            } else if (role == "student")
            {
                if(Student != null)
                {
                    return !string.IsNullOrEmpty(FirstName) &&
                           !string.IsNullOrEmpty(LastName) &&
                           !string.IsNullOrEmpty(PhoneNumber) &&
                           !string.IsNullOrEmpty(Student.Faculty) &&
                           !string.IsNullOrEmpty(Student.University) &&
                           !string.IsNullOrEmpty(Student.Degree) &&
                           Student.YearOfStudy > 0 && Student.CvId != Guid.Empty;
                }
            }
            return false;
        }
    }
}
