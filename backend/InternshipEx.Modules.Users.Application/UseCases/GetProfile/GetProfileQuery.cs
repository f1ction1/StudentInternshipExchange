using InternshipEx.Modules.Users.Domain.DTOs;
using MediatR;
using Modules.Common.Application.Messaging;

namespace InternshipEx.Modules.Users.Application.UseCases.GetProfile
{
    public record GetProfileQuery() : IQuery<ProfileDto>;
}
