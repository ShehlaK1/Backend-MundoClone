using Repository.Repositories;

namespace RESTCore.ServiceExtensions
{
    public static class ServiceRegistryExtention
    {
        public static IServiceCollection RegisterRepositories(
           this IServiceCollection services,
           IWebHostEnvironment _env)
        {
            services.AddScoped<UserRepo>();
            services.AddScoped<YearRepo>();
            services.AddScoped<GradeRepo>();
            services.AddScoped<AssessmentRepo>();
            services.AddScoped<TargetSkillRepo>();
            services.AddScoped<CompositeLevelRepo>();
            services.AddScoped<LessonRepo>();


            return services;
        }
    }
}
