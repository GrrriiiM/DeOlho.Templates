using DeOlho.Templates.Application;
using DeOlho.Templates.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;

namespace DeOlho.Templates.API
{
    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder UseMigrate(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var deOlhoDbContext = serviceScope.ServiceProvider.GetService<DeOlhoDbContext>())
                {
                    deOlhoDbContext.Database.Migrate();
                }
            }

            return app;
        }

        public static IApplicationBuilder UseEventBus(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                var busClient = serviceScope.ServiceProvider.GetService<IBusClient>();
                IntegrationEvent.Subscribe(busClient, message => 
                {
                    using (var mediatorServiceScope = app.ApplicationServices
                        .GetRequiredService<IServiceScopeFactory>()
                        .CreateScope())
                    {
                        return mediatorServiceScope.ServiceProvider.GetService<IMediator>().Publish(message);
                    }
                });
            }

            return app;
        }
    }
}