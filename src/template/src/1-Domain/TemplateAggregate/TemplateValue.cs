using System;
using DeOlho.Templates.Domain.SeedWork;

namespace DeOlho.Templates.Domain.TemplateAggregate
{
    public class TemplateValue : ValueObject
    {
        public TemplateValue(string description)
        {
            Description = description ?? throw new ArgumentNullException(nameof(description));
        }
        public string Description { get; private set; }
    }
}