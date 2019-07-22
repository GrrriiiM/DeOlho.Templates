using System;
using System.Linq;
using DeOlho.Templates.Application;
using DeOlho.Templates.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using RawRabbit;

namespace DeOlho.Templates.API
{
    public static class ServiceCollectionExtension
    {
        public static IHttpClientBuilder AddRetryPolicy(this IHttpClientBuilder value)
        {
            return value.AddPolicyHandler((_) =>
                HttpPolicyExtensions
                    .HandleTransientHttpError()
                    .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                    .WaitAndRetryAsync(new TimeSpan[] {
                        TimeSpan.FromSeconds(1),
                        TimeSpan.FromSeconds(5),
                        TimeSpan.FromSeconds(10)
                    }));
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            var types = typeof(IRepository<>).Assembly.GetTypes();

            foreach(var irepository in types.Where(_ => _.IsInterface))
            {
                var repository = types.SingleOrDefault(_ => irepository.IsAssignableFrom(_));
                if (repository != null)
                    services.AddScoped(irepository, repository);
            }

            return services;
        }

        public static IServiceCollection AddEventBusSubscribe<TMessage>(this IServiceCollection services)
        {
            using (var serviceScopeEventBus = services.BuildServiceProvider().CreateScope())
            {
                var busClient = serviceScopeEventBus.ServiceProvider.GetService<IBusClient>();

                busClient.SubscribeAsync<TMessage>(async (message, messageContext) => 
                {
                    using (var serviceScopeMediator = services.BuildServiceProvider().CreateScope())
                    {
                        var mediator = serviceScopeEventBus.ServiceProvider.GetService<IMediator>();
                        await mediator.Publish(new IntegrationEventMessage<TMessage>(message));
                    }
                });
            }
            return services;
        }
    }
}