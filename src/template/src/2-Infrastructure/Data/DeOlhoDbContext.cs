using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DeOlho.Templates.Domain.SeedWork;
using DeOlho.Templates.Domain.TemplateAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DeOlho.Templates.Infrastructure.Data
{
    public class DeOlhoDbContext: DbContext, IUnitOfWork, IDeOlhoQueryProvider
    {

        private readonly IMediator _mediator;

        private DeOlhoDbContext(DbContextOptions<DeOlhoDbContext> options) : base(options) { }

        public DeOlhoDbContext(DbContextOptions<DeOlhoDbContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        IQueryable<TEntity> IDeOlhoQueryProvider.Query<TEntity>()
        {
            return Set<TEntity>();
        }

        public override int SaveChanges()
        {
            _mediator.DispatchDomainEventsAsync(this).Wait();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            _mediator.DispatchDomainEventsAsync(this).Wait();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await _mediator.DispatchDomainEventsAsync(this);
            return await base.SaveChangesAsync(cancellationToken);
        }

        public async override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            await _mediator.DispatchDomainEventsAsync(this);
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}