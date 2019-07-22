using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeOlho.Templates.Domain.SeedWork;
using MediatR;

namespace DeOlho.Templates.Infrastructure.Data
{
    static class MediatorExtension
    {
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, DeOlhoDbContext deOlhoDbContext)
        {
            var domainEvents = ListDomainEvents(deOlhoDbContext);

            while(domainEvents.Any())
            {
                var tasks = domainEvents
                    .Select(async (domainEvent) => {
                        await mediator.Publish(domainEvent);
                    });

                await Task.WhenAll(tasks);

                domainEvents = ListDomainEvents(deOlhoDbContext);
            }

            
        }

        public static List<INotification> ListDomainEvents(DeOlhoDbContext deOlhoDbContext)
        {
            var domainEntities = deOlhoDbContext.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            return domainEvents;
        }
    }
}