using System.Data.Common;
using Dapper;
using Greenlight.Common.Application.Data;
using Greenlight.Common.Application.Messaging;
using Greenlight.Common.Domain;
using Greenlight.Modules.Initiatives.Application.Initiatives.GetInitiative;
using Greenlight.Modules.Initiatives.Domain.Initiatives;

namespace Greenlight.Modules.Initiatives.Application.Initiatives.SearchInitiatives;

internal sealed class SearchInitiativesQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<SearchInitiativesQuery, SearchInitiativesResponse>
{

    public async Task<Result<SearchInitiativesResponse>> Handle(
        SearchInitiativesQuery request,
        CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        var parameters = new SearchEventsParameters(
            // :DLO:1 Change SearchInitiativesQuery to support InitiativeProcessStatus as parameter
            (int)InitiativeProcessStatus.Approved,
            request.CategoryId,
            request.Title,
            request.Description,
            request.PageSize,
            (request.Page - 1) * request.PageSize);

        IReadOnlyCollection<InitiativeResponse> initiatives = await GetInitiativesAsync(connection, parameters);

        int totalCount = await CountInitiativesAsync(connection, parameters);

        return new SearchInitiativesResponse(request.Page, request.PageSize, totalCount, initiatives);
    }

    private static async Task<IReadOnlyCollection<InitiativeResponse>> GetInitiativesAsync(
        DbConnection connection,
        SearchEventsParameters parameters)
    {
        const string sql =
            $"""
             SELECT
                 id AS {nameof(InitiativeResponse.Id)},
                 title AS {nameof(InitiativeResponse.Title)},
                 description AS {nameof(InitiativeResponse.Description)},
                 category_id AS {nameof(InitiativeResponse.CategoryId)}
             FROM initiatives.initiatives
             WHERE
                status = @ProcessStatus AND
                (@CategoryId IS NULL OR category_id = @CategoryId)
             ORDER BY title
             OFFSET @Skip
             LIMIT @Take
             """;

        List<InitiativeResponse> initiatives = (await connection.QueryAsync<InitiativeResponse>(sql, parameters))
            .AsList();

        return initiatives;
    }

    private static async Task<int> CountInitiativesAsync(DbConnection connection, SearchEventsParameters parameters)
    {
        const string sql =
            """
            SELECT COUNT(*)
            FROM initiatives.initiatives
            WHERE
               status = @ProcessStatus AND
               (@CategoryId IS NULL OR category_id = @CategoryId)
            """;

        int totalCount = await connection.ExecuteScalarAsync<int>(sql, parameters);

        return totalCount;
    }

    private sealed record SearchEventsParameters(
        int Status,
        Guid? CategoryId,
        string? Title,
        string? Description,
        int Take,
        int Skip);
}

