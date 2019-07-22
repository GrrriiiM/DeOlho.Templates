using System.Collections.Generic;
using DeOlho.Templates.DataContract.TemplateAggregate.Models;
using MediatR;

namespace DeOlho.Templates.DataContract.TemplateAggregate.Queries
{
    public class ListTemplateQuery : IRequest<IEnumerable<TemplateModel>>
    {
        public string Description { get; set; }
    }
}