using InternshipEx.Recommendations.Persistence;
using Modules.Common.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InternshipEx.Recommendations.Infrastructure
{
    public static class RecommendationModuleInstaller
    {
        public static IServiceCollection AddRecommendationModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RecDbContext>((sp, options) =>
            {
                options.UseSqlServer(configuration.GetConnectionString("AuthModuleDb"));
            });
            services.AddScoped<CurrentUserService>();
            return services;
        }
    }
}
