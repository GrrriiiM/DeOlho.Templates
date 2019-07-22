using System;
using System.Collections.Generic;
using System.Linq;
using DeOlho.Templates.Domain.Events;
using DeOlho.Templates.Domain.SeedWork;

namespace DeOlho.Templates.Domain.TemplateAggregate
{
    public class Template : Entity
    {
        public Template(
            string descricao,
            TemplateValue value) 
        {
            Status = TemplateStatus.Ativo;
            _statusId = TemplateStatus.Ativo.Id;
            Update(
                descricao, 
                value);
        }

        public string Description { get; private set; }
        public int _statusId;
        public TemplateStatus Status { get; private set; }
        public TemplateValue Value { get; private set; }

        private List<TemplateItem> _itens = new List<TemplateItem>();
        public virtual IReadOnlyCollection<TemplateItem> Itens => _itens.AsReadOnly();

        public void Update(
            string descricao,
            TemplateValue value)
        {
            Description = descricao ?? throw new ArgumentNullException(nameof(Description));
            Value = value ?? throw new ArgumentNullException(nameof(Value));

            if (Id == 0)
                AddDomainEvent(new TemplateCreatedDomainEvent(this));
            else
                AddDomainEvent(new TemplateChangedDomainEvent(this));
        }

        public TemplateItem AddItem(string descricao)
        {
            var item = new TemplateItem(this, descricao);
            _itens.Add(item);
            return item;
        }

        public void RemoveItem(long itemId)
        {
            if (_itens.Any(_ => _.Id == itemId))
            {
                throw new ArgumentOutOfRangeException(nameof(Itens));
            }
            _itens.RemoveAll(_ => _.Id == itemId);
        }

        public void RemoveItem(TemplateItem item)
        {
            RemoveItem(item.Id);
        }

        public void Cancel()
        {
            Status = TemplateStatus.Cancelado;
            _statusId = TemplateStatus.Cancelado.Id;
            AddDomainEvent(new TemplateCanceledDomainEvent(this));
        }
    }
}