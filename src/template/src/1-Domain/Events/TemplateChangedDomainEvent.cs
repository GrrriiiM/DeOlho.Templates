using System;
using DeOlho.Templates.Domain.TemplateAggregate;
using MediatR;

namespace DeOlho.Templates.Domain.Events
{
    public class TemplateChangedDomainEvent : INotification
    {
        public TemplateChangedDomainEvent(
            Template template)
        {
            Template = template ?? throw new ArgumentNullException(nameof(Template));
        }
        public Template Template { get; private set; }
    }
}