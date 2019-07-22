using DeOlho.Templates.Domain.TemplateAggregate;
using DeOlho.Templates.Infrastructure.Data;

namespace DeOlho.Templates.Infrastructure.Repositories
{
    public class TemplateRepository : Repository<Template>
    {
        public TemplateRepository(DeOlhoDbContext dbContext)
         : base(dbContext) {}
    }
}