using System.Threading;
using System.Threading.Tasks;
using DeOlho.Templates.DataContract.TemplateAggregate.Commands;
using DeOlho.Templates.Domain.TemplateAggregate;
using DeOlho.Templates.Infrastructure.Repositories;
using MediatR;

namespace DeOlho.Templates.Application.TemplateAggregate.Commands
{
    public class CreateTemplateCommandHandler : IRequestHandler<CreateTemplateCommand>
    {
        readonly ITemplateRepository _templateRepository;

        public CreateTemplateCommandHandler(
            ITemplateRepository templateRepository)
        {
            _templateRepository = templateRepository;
        }

        public async Task<Unit> Handle(CreateTemplateCommand request, CancellationToken cancellationToken)
        {
            var templateValue = new TemplateValue(request.descriptionValue);
            var template = new Template(request.description, templateValue);
            await _templateRepository.AddAsync(template);
            await _templateRepository.UnityOfWork.SaveChangesAsync();

            return await Unit.Task;
        }
    }
}