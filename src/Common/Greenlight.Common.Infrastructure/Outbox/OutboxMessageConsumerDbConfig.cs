using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Greenlight.Common.Infrastructure.Outbox;

public sealed class OutboxMessageConsumerDbConfig : IEntityTypeConfiguration<OutboxMessageConsumer>
{
    public void Configure(EntityTypeBuilder<OutboxMessageConsumer> builder)
    {
        builder.ToTable("outbox_message_consumers");

        builder.HasKey(o => new { o.OutboxMessageId, o.Name });

        builder.Property(o => o.Name)
            .HasMaxLength(MessagingConstants.MaxMessageNameLength);
    }
}
