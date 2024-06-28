using System.Data.Common;
using Dapper;
using Greenlight.Common.Application.Data;
using Greenlight.Common.Application.EventBus;
using Greenlight.Common.Infrastructure.Inbox;
using Greenlight.Modules.Initiatives.Infrastructure.Database;

namespace Greenlight.Modules.Initiatives.Infrastructure.Inbox;

internal sealed class IdempotentIntegrationEventHandler<TIntegrationEvent>(
    IIntegrationEventHandler<TIntegrationEvent> decorated,
    IDbConnectionFactory dbConnectionFactory)
    : IntegrationEventHandler<TIntegrationEvent>
    where TIntegrationEvent : IIntegrationEvent
{
    private const string Schema = Schemas.Initiatives;

    public override async Task Handle(
        TIntegrationEvent integrationEvent,
        CancellationToken cancellationToken = default)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        var inboxMessageConsumer = new InboxMessageConsumer(integrationEvent.Id, decorated.GetType().Name);

        if (await InboxConsumerExistsAsync(connection, inboxMessageConsumer))
        {
            return;
        }

        await decorated.Handle(integrationEvent, cancellationToken);

        await InsertInboxConsumerAsync(connection, inboxMessageConsumer);
    }

    private static async Task<bool> InboxConsumerExistsAsync(
        DbConnection dbConnection,
        InboxMessageConsumer inboxMessageConsumer)
    {
        const string sql =
            $"""
             SELECT EXISTS(
                 SELECT 1
                 FROM {Schema}.inbox_message_consumers
                 WHERE inbox_message_id = @InboxMessageId AND
                       name = @Name
             )
             """;

        return await dbConnection.ExecuteScalarAsync<bool>(sql, inboxMessageConsumer);
    }

    private static async Task InsertInboxConsumerAsync(
        DbConnection dbConnection,
        InboxMessageConsumer inboxMessageConsumer)
    {
        const string sql =
            $"""
             INSERT INTO {Schema}.inbox_message_consumers(inbox_message_id, name)
             VALUES (@InboxMessageId, @Name)
             """;

        await dbConnection.ExecuteAsync(sql, inboxMessageConsumer);
    }
}
