using DeOlho.Templates.DataContract.TemplateAggregate.Models;
using DeOlho.Templates.Domain.TemplateAggregate;

namespace DeOlho.Templates.Application.ModelAdapters
{
    public class TemplateModelAdapter : TemplateModel
    {
        public TemplateModelAdapter(Template template)
        {
            Id = template.Id;
            Description = template.Description;
            StatusName = template.Status.Name;
            ValueDescription = template.Value.Description;
        }
    }
}