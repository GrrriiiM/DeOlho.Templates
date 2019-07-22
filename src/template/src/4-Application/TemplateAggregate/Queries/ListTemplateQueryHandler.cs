using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DeOlho.Templates.Application.ModelAdapters;
using DeOlho.Templates.DataContract.TemplateAggregate.Models;
using DeOlho.Templates.DataContract.TemplateAggregate.Queries;
using DeOlho.Templates.Domain.TemplateAggregate;
using DeOlho.Templates.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DeOlho.Templates.Application.TemplateAggregate.Queries
{
    public class ListTemplateQueryHandler : IRequestHandler<ListTemplateQuery, IEnumerable<TemplateModel>>
    {
        readonly IDeOlhoQueryProvider _queryProvider;

        public ListTemplateQueryHandler(
            IDeOlhoQueryProvider queryProvider)
        {
            _queryProvider = queryProvider;
        }

        public async Task<IEnumerable<TemplateModel>> Handle(ListTemplateQuery request, CancellationToken cancellationToken)
        {
            var templates = await _queryProvider.Query<Template>()
                .Where(_ => _.Description.Contains(request.Description))
                .ToListAsync();
            return templates.Select(_ => new TemplateModelAdapter(_));
        }
    }
}