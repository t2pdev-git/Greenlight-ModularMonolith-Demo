using Greenlight.Modules.Initiatives.Domain.Categories;
using Greenlight.Modules.Initiatives.Domain.Initiatives;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Greenlight.Modules.Initiatives.Infrastructure.Initiatives;
internal sealed class InitiativeDbConfig : IEntityTypeConfiguration<Initiative>
{
    public void Configure(EntityTypeBuilder<Initiative> builder)
    {
        builder.HasOne<Category>().WithMany();

        builder.Property(i => i.Title)
            .HasMaxLength(Initiative.TitleMaxLength);
    }
}
