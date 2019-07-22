using System;
using DeOlho.Templates.Domain.Events;
using DeOlho.Templates.Domain.SeedWork;

namespace DeOlho.Templates.Domain.TemplateAggregate
{
    public class TemplateItem : Entity
    {
        public TemplateItem(
            Template template,
            string descricao) 
        {
            Template = template ?? throw new ArgumentNullException(nameof(Template));
            Update(descricao);
        }

        public virtual Template Template { get; private set; }
        public string Descricao { get; private set; } 

        public void Update(
            string descricao)
        {
            Descricao = descricao ?? throw new ArgumentNullException(nameof(Descricao));;
            
            if (Id == 0)
                AddDomainEvent(new TemplateItemCreatedDomainEvent(this));
            else
                AddDomainEvent(new TemplateItemChangedDomainEvent(this));
        }
    }
}