using DeOlho.Templates.Domain.SeedWork;

namespace DeOlho.Templates.Domain.TemplateAggregate
{
    public class TemplateStatus : Enumeration
    {
        public TemplateStatus(int id, string name)
            : base(id, name)
        {}

        public static TemplateStatus Ativo = new TemplateStatus(1, nameof(Ativo));
        public static TemplateStatus Cancelado = new TemplateStatus(99, nameof(Cancelado));
    }
}