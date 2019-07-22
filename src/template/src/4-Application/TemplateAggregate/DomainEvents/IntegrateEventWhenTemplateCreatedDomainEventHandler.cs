using System.Threading;
using System.Threading.Tasks;
using DeOlho.Templates.Domain.Events;
using MediatR;
using RawRabbit;

namespace DeOlho.Templates.Application.TemplateAggregate.DomainEvents
{
    public class IntegrateEventWhenTemplateCreatedDomainEventHandler : INotificationHandler<TemplateCreatedDomainEvent>
    {
        readonly IBusClient _busClient;

        public IntegrateEventWhenTemplateCreatedDomainEventHandler(
            IBusClient busClient)
        {
            _busClient = busClient;
        }

        public async Task Handle(TemplateCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var message = new TemplateCreatedMessage
            {
                Description = notification.Template.Description
            };
            await _busClient.PublishAsync(new IntegrationEventMessage<TemplateCreatedMessage>(message));
        }
    }
}