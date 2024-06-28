using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Greenlight.Common.Infrastructure.Inbox;

public sealed class InboxMessageDbConfig : IEntityTypeConfiguration<InboxMessage>
{
    public void Configure(EntityTypeBuilder<InboxMessage> builder)
    {
        builder.ToTable("inbox_messages");

        builder.HasKey(o => o.Id);

        builder.Property(o => o.Content)
            .HasMaxLength(MessagingConstants.MaxMessageContentLength)
            .HasColumnType("jsonb");
    }
}
