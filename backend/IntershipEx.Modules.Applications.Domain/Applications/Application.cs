using IntershipEx.Modules.Applications.Domain.Applications.Events;
using Modules.Common.Domain.Abstractions;

namespace IntershipEx.Modules.Applications.Domain.Applications
{
    public class Application : Entity<Guid>
    {
        public Guid IntershipId { get; private set; }
        public Guid StudentId { get; private set; } // Profile.Id
        public string? CoverLetter { get; private set; }
        public ApplicationStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; } 
        public Review? Review { get; private set; }
        public Reject? Reject { get; private set; }
        public InternshipInfo InternshipInfo { get; private set; } = null!;

        public static Application Create(
            Guid intershipId,
            Guid studentId,
            string? coverLetter,
            InternshipInfo internshipInfo)
        {
            var application = new Application
            {
                Id = Guid.NewGuid(),
                IntershipId = intershipId,
                StudentId = studentId,
                CoverLetter = coverLetter,
                Status = ApplicationStatus.Applied,
                CreatedAt = DateTime.UtcNow,
                InternshipInfo = internshipInfo
            };

            application.RaiseDomainEvent(new ApplicationAppliedDomainEvent(Guid.NewGuid(), intershipId, studentId, application.CreatedAt));

            return application;
        }

        public Result MakeReview(string? reviewNotes)
        {
            if(Status == ApplicationStatus.Reviewed)
            {
                return Result.Failure(ApplicationErrors.AlreadyReviewed);
            }
            Review = new Review(DateTime.UtcNow, reviewNotes!);
            Status = ApplicationStatus.Reviewed;
            RaiseDomainEvent(new ApplicationReviewedDomainEvent(Guid.NewGuid(), Id, Review.ReviewedAt));

            return Result.Success();
        }
        public Result RejectApplication(string? rejectionReason)
        {
            if (Status == ApplicationStatus.Rejected)
            {
                return Result.Failure(ApplicationErrors.AlreadyRejected);
            }

            Status = ApplicationStatus.Rejected;
            Reject = new Reject(DateTime.UtcNow, rejectionReason);

            //No handler for this event
            RaiseDomainEvent(new ApplicationRejectedDomainEvent(
                Guid.NewGuid(),
                Id,
                StudentId,
                IntershipId,
                rejectionReason));

            return Result.Success();
        }
    }
}
