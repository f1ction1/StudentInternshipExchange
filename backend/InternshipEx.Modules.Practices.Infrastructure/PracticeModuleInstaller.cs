using InternshipEx.Modules.Practices.Application.Interfaces;
using InternshipEx.Modules.Practices.Application.UseCases.Dictionaries.GetAllInternshipDictionaries;
using InternshipEx.Modules.Practices.Contracts;
using InternshipEx.Modules.Practices.Persistence;
using InternshipEx.Modules.Practices.Persistence.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using InternshipEx.Modules.Practices.Application.InternalApi;


namespace InternshipEx.Modules.Practices.Infrastructure
{
    public static class PracticeModuleInstaller
    {
        public static IServiceCollection AddPracticesModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PracticesDbContext>((sp, options) =>
            {
                options.UseSqlServer(configuration.GetConnectionString("AuthModuleDb"));
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IInternshipModuleApi, IntershipModuleApi>();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAllInternshipDictionariesQuery).Assembly));

            return services;
        }
    }
}
