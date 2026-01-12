using FluentValidation;
using InternshipEx.Modules.Users.Application.Interfaces;
using InternshipEx.Modules.Users.Application.PublicApi;
using InternshipEx.Modules.Users.Application.UseCases.UpsertProfile;
using InternshipEx.Modules.Users.Contracts;
using InternshipEx.Modules.Users.Endpoints;
using InternshipEx.Modules.Users.Persistence;
using InternshipEx.Modules.Users.Persistence.UnitOfWork;
using MediatR;
using MediatR.Extensions.FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InternshipEx.Modules.Users.Infrastructure
{
    public static class UserModuleInstaller
    {
        public static IServiceCollection AddUserModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(UpsertProfileCommand).Assembly));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddValidatorsFromAssemblyContaining<UpsertProfileCommandValidator>();

            services.AddScoped<IUsersPublicApi, UsersPublicApi>();

            services.AddDbContext<UsersDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("UserModuleDb"));
            });

            return services;
        }
    }
}
