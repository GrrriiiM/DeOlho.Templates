using System.Collections.Generic;
using System.Threading.Tasks;
using DeOlho.Templates.DataContract.TemplateAggregate.Commands;
using DeOlho.Templates.DataContract.TemplateAggregate.Models;
using DeOlho.Templates.DataContract.TemplateAggregate.Queries;

namespace DeOlho.Templates.DataContract.TemplateAggregate
{
    public interface ITemplateService
    {
         Task CreateTemplate(CreateTemplateCommand command);
         Task<IEnumerable<TemplateModel>> ListTemplate(ListTemplateQuery query);
    }
}