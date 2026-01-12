using InternshipEx.Modules.Auth.Application.DTOs;
using InternshipEx.Modules.Auth.Application.Interfaces;
using Modules.Common.Application.Exceptions;
using MediatR;

namespace InternshipEx.Modules.Auth.Application.UseCases.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, AuthResponseDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenService _tokenService;
        private readonly IPasswordHasher _passwordHasher;
        public LoginUserCommandHandler(IUserRepository userService, IJwtTokenService tokenService, IPasswordHasher passwordHasher)
        {
            _userRepository = userService;
            _tokenService = tokenService;
            _passwordHasher = passwordHasher;
        }
        public async Task<AuthResponseDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
            
            if(user == null)
            {
                //User with this email does not exist.
                throw new UnauthorizedException("Invalid email or password.");
            }

            if (!_passwordHasher.Verify(request.Password, user.PasswordHash))
            {
                //Invalid password.
                throw new UnauthorizedException("Invalid email or password.");
            }

            var token = _tokenService.CreateToken(user);
            return new AuthResponseDto
            {
                IsSuccess = true,
                UserId = user.Id,
                Token = token
            };
        }
    }
}
