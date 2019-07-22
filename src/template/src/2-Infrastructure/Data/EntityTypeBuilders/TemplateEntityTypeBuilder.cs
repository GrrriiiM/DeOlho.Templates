using DeOlho.Templates.Domain.TemplateAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeOlho.Templates.Infrastructure.Data.EntityBuilders
{
    public class TemplateEntityTypeBuilder : IEntityTypeConfiguration<Template>
    {
        public void Configure(EntityTypeBuilder<Template> builder)
        {
            builder.HasKey(_ => _.Id);
            builder.Metadata.FindNavigation(nameof(Template.Itens))
                .SetPropertyAccessMode(PropertyAccessMode.Field);
            builder.HasOne(o => o.Status)
                .WithMany()
                .HasForeignKey($"{nameof(Template.Status)}Id");
            builder.OwnsOne(_ => _.Value);
        }
    }
}