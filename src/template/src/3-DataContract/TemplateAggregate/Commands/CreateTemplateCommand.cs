using MediatR;

namespace DeOlho.Templates.DataContract.TemplateAggregate.Commands
{
    public class CreateTemplateCommand : IRequest
    {
        public string description { get; set; }
        public string descriptionValue { get; set; }
    }
}