using CurrencyRateAdapter.Presentation.Common.Errors;
using Mapster;
using MapsterMapper;
using System.Reflection;

namespace CurrencyRateAdapter.Presentation
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(
            this IServiceCollection services)
        {
            services.AddControllers();

            services
                .AddMappings()
                .AddSwaggerGen()
                .AddGlobalExcetionHandling()
                ;

            return services;
        }

        public static IServiceCollection AddMappings(
           this IServiceCollection services)
        {
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(Assembly.GetExecutingAssembly());

            services.AddSingleton(config);

            services.AddScoped<IMapper, ServiceMapper>();

            return services;
        }

        public static IServiceCollection AddGlobalExcetionHandling(
           this IServiceCollection services)
        {
            services
                .AddExceptionHandler<GlobalExceptionHandler>()
                .AddProblemDetails();

            return services;
        }
    }
}
