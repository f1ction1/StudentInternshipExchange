using Internship.Modules.Auth.IntegrationEvents;
using InternshipEx.Modules.Auth.Application.DTOs;
using InternshipEx.Modules.Auth.Application.Interfaces;
using InternshipEx.Modules.Auth.Domain.DomainEvents;
using InternshipEx.Modules.Auth.Domain.Entities;
using Modules.Common.Application.Exceptions;
using MassTransit;
using MediatR;

namespace InternshipEx.Modules.Auth.Application.UseCases.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, AuthResponseDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenService _tokenService;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IPublishEndpoint _publishEndpoint;
        public RegisterUserCommandHandler(
            IUserRepository userService,
            IJwtTokenService tokenService,
            IPasswordHasher passwordHasher,
            IPublishEndpoint publishEndpoint)
        {
            _userRepository = userService;
            _tokenService = tokenService;
            _passwordHasher = passwordHasher;
            _publishEndpoint = publishEndpoint;

        }
        public async Task<AuthResponseDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
            if(existingUser != null)
            {
                //User with this email already exists.
                throw new ConflictException("User with this email already exists.");
            }

            var hashedPassword = _passwordHasher.Hash(request.Password);
            var user = User.Create(request.Email, hashedPassword, request.Role);
            try
            {
                await _userRepository.AddAsync(user, cancellationToken);     
                var token = _tokenService.CreateToken(user);
                await _userRepository.SaveChangesAsync(cancellationToken);

                return new AuthResponseDto() { IsSuccess = true, UserId = user.Id, Token = token };
            } catch
            {
                //An error occurred while registering the user.
                throw new Exception("An error occurred while registering the user.");
            }
        }
    }
}
