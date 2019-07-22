using System;
using DeOlho.Templates.Domain.TemplateAggregate;
using MediatR;

namespace DeOlho.Templates.Domain.Events
{
    public class TemplateItemChangedDomainEvent : INotification
    {
        public TemplateItemChangedDomainEvent(
            TemplateItem itemtemplate)
        {
            ItemTemplate = itemtemplate ?? throw new ArgumentNullException(nameof(Template));
        }
        public TemplateItem ItemTemplate { get; private set; }
    }
}