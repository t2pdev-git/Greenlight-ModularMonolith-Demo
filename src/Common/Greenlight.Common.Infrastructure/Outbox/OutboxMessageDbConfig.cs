using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Greenlight.Common.Infrastructure.Outbox;

public sealed class OutboxMessageDbConfig : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.ToTable("outbox_messages");

        builder.HasKey(o => o.Id);

        builder.Property(o => o.Type)
            .HasMaxLength(150);

        builder.Property(o => o.Content)
            .HasMaxLength(MessagingConstants.MaxMessageContentLength)
            .HasColumnType("jsonb");
    }
}
