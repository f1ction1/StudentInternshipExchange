using InternshipEx.Modules.Auth.Application.Interfaces;
using InternshipEx.Modules.Auth.Application.UseCases.RegisterUser;
using InternshipEx.Modules.Auth.Endpoints;
using InternshipEx.Modules.Auth.Infrastructure.Services;
using InternshipEx.Modules.Auth.Persistence;
using InternshipEx.Modules.Auth.Persistence.Interceptors;
using InternshipEx.Modules.Auth.Persistence.Outbox;
using InternshipEx.Modules.Auth.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MediatR;

namespace InternshipEx.Modules.Auth.Infrastructure
{
    public static class AuthModuleInstaller
    {
        public static IServiceCollection AddAuthModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(RegisterUserCommand).Assembly));

            services.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();
            services.AddDbContext<AuthDbContext>((sp, options) =>
            {
                var interceptor = sp.GetRequiredService<ConvertDomainEventsToOutboxMessagesInterceptor>();
                options.UseSqlServer(configuration.GetConnectionString("AuthModuleDb"))
                       .AddInterceptors(interceptor);
            });

            services.AddHostedService<OutboxBackgroundService>();
            services.AddScoped<OutboxProcessor>();

            services.AddControllers().
                AddApplicationPart(typeof(AuthController).Assembly);

            return services;
        }
    }
}
