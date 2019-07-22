using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace DeOlho.Templates.Application.TemplateAggregate.IntegrationEvents
{
    public class TemplateCreatedIntegrationEventHandler : INotificationHandler<IntegrationEventMessage<TemplateCreatedMessage>>
    {
        public Task Handle(IntegrationEventMessage<TemplateCreatedMessage> notification, CancellationToken cancellationToken)
        {
            System.Diagnostics.Debug.WriteLine($"Integration Event {nameof(TemplateCreatedIntegrationEventHandler)}: {notification.Message.Description}");

            return Unit.Task;
        }
    }


}