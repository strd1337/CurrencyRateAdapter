using CurrencyRateAdapter.Domain.Common.Constants;
using CurrencyRateAdapter.Presentation.Common.Errors;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.RateLimiting;
using System.Reflection;
using System.Threading.RateLimiting;

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
                .AddRateLimiter()
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

        public static IServiceCollection AddRateLimiter(
            this IServiceCollection services)
        {
            services.AddRateLimiter(rateLimiterOptions =>
            {
                rateLimiterOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

                rateLimiterOptions.AddTokenBucketLimiter(
                    Constants.TokenBucketLimiter.PolicyName.PerChatLimit,
                    options =>
                    {
                        options.TokenLimit = 1;
                        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                        options.QueueLimit = 5;
                        options.ReplenishmentPeriod = TimeSpan.FromSeconds(10);
                        options.TokensPerPeriod = 10;
                        options.AutoReplenishment = true;
                    }
                );

                rateLimiterOptions.AddTokenBucketLimiter(
                    Constants.TokenBucketLimiter.PolicyName.GlobalLimit,
                    options =>
                    {
                        options.TokenLimit = 1;
                        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                        options.QueueLimit = 0;
                        options.ReplenishmentPeriod = TimeSpan.FromSeconds(1);
                        options.TokensPerPeriod = 1;
                        options.AutoReplenishment = true;
                    }
                );

                rateLimiterOptions.AddTokenBucketLimiter(
                    Constants.TokenBucketLimiter.PolicyName.PerGroupLimit,
                    options =>
                    {
                        options.TokenLimit = 20;
                        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                        options.QueueLimit = 5;
                        options.ReplenishmentPeriod = TimeSpan.FromMinutes(1);
                        options.TokensPerPeriod = 20;
                        options.AutoReplenishment = true;
                    }
                );
            });

            return services;
        }
    }
}
