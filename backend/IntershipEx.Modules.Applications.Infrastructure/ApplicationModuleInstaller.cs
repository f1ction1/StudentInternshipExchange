using InternshipEx.Modules.Applications.Contracts;
using InternshipEx.Modules.Practices.Contracts;
using IntershipEx.Modules.Applications.Application.Abstractions.Data;
using IntershipEx.Modules.Applications.Application.InternalApi;
using IntershipEx.Modules.Applications.Application.UseCases.Applications.ApplyInternship;
using IntershipEx.Modules.Applications.Persistence;
using IntershipEx.Modules.Applications.Persistence.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IntershipEx.Modules.Applications.Infrastructure
{
    public static class ApplicationModuleInstaller
    {
        public static IServiceCollection AddApplicationModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationsDbContext>((sp, options) =>
            {
                options.UseSqlServer(configuration.GetConnectionString("AuthModuleDb"));
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IApplicationsModuleApi, ApplicationsModuleApi>();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplyInternshipCommand).Assembly));

            return services;
        }
    }

}
