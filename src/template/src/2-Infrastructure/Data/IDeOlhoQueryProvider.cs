using System.Linq;
using DeOlho.Templates.Domain.SeedWork;

namespace DeOlho.Templates.Infrastructure.Data
{
    public interface IDeOlhoQueryProvider
    {
         IQueryable<TEntity> Query<TEntity>() where TEntity : Entity;
    }
}