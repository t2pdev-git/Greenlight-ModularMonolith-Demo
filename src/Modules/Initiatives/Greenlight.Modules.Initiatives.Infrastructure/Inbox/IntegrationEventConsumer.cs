using System.Data.Common;
using Dapper;
using Greenlight.Common.Application.Data;
using Greenlight.Common.Application.EventBus;
using Greenlight.Common.Infrastructure.Inbox;
using Greenlight.Common.Infrastructure.Serialization;
using Greenlight.Modules.Initiatives.Infrastructure.Database;
using MassTransit;
using Newtonsoft.Json;

namespace Greenlight.Modules.Initiatives.Infrastructure.Inbox;

internal sealed class IntegrationEventConsumer<TIntegrationEvent>(IDbConnectionFactory dbConnectionFactory)
    : IConsumer<TIntegrationEvent>
    where TIntegrationEvent : IntegrationEvent
{
    public async Task Consume(ConsumeContext<TIntegrationEvent> context)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        TIntegrationEvent integrationEvent = context.Message;

        var inboxMessage = new InboxMessage
        {
            Id = integrationEvent.Id,
            Type = integrationEvent.GetType().Name,
            Content = JsonConvert.SerializeObject(integrationEvent, SerializerSettings.Instance),
            OccurredOnUtc = integrationEvent.OccurredOnUtc
        };

        const string sql =
            $"""
            INSERT INTO {Schemas.Initiatives}.inbox_messages(id, type, content, occurred_on_utc)
            VALUES (@Id, @Type, @Content::json, @OccurredOnUtc)
            """;

        await connection.ExecuteAsync(sql, inboxMessage);
    }
}
