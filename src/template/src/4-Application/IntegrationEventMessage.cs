using System;
using System.Threading.Tasks;
using DeOlho.Templates.Application.TemplateAggregate;
using MediatR;
using RawRabbit;

namespace DeOlho.Templates.Application
{

    public static class IntegrationEvent
    {
        public static void Subscribe(IBusClient busClient, Func<INotification, Task> mediatorPublish)
        {
            busClient.SubscribeAsync<TemplateCreatedMessage>(async (message, messageContext) => 
            {
                await mediatorPublish(new IntegrationEventMessage<TemplateCreatedMessage>(message));
            });
        }
    }

    public class IntegrationEventMessage<TMessage> : INotification
    {
        

        public IntegrationEventMessage(TMessage message) => Message = message;
        public TMessage Message { get; set; }
    }
}