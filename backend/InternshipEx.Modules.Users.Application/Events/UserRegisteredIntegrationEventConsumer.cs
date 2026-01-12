using Internship.Modules.Auth.IntegrationEvents;
using InternshipEx.Modules.Users.Application.Interfaces;
using InternshipEx.Modules.Users.Domain.Entities;
using MassTransit;

namespace InternshipEx.Modules.Users.Application.Events
{
    public class UserRegisteredIntegrationEventConsumer(
        IUnitOfWork unitOfWork) : IConsumer<UserRegisteredIntegrationEvent>
    {
        public async Task Consume(ConsumeContext<UserRegisteredIntegrationEvent> context)
        {
            Profile? existingProfile = await unitOfWork.Profiles.GetByIdAsync(context.Message.UserId);
            if(existingProfile != null)
            {
                return;
            }
            var profile = Profile.Create(context.Message.UserId);
            await unitOfWork.Profiles.AddAsync(profile);

            if(context.Message.Role == "student")
            {
                var existingStudent = await unitOfWork.Students.GetByIdAsync(context.Message.UserId);
                if (existingStudent == null)
                {
                    var student = Student.Create(context.Message.UserId);
                    await unitOfWork.Students.AddAsync(student);
                }
            }

            await unitOfWork.SaveChangesAsync();
        }
    }
}
