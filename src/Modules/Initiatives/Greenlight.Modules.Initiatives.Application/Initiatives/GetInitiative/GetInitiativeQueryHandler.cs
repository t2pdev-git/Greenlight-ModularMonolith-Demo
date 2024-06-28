using System.Data.Common;
using Dapper;
using Greenlight.Common.Application.Data;
using MediatR;

namespace Greenlight.Modules.Initiatives.Application.Initiatives.GetInitiative;

internal sealed class GetInitiativeQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IRequestHandler<GetInitiativeQuery, InitiativeResponse?>
{
    public async Task<InitiativeResponse?> Handle(GetInitiativeQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
              SELECT
                  id AS {nameof(InitiativeResponse.Id)},
                  title AS {nameof(InitiativeResponse.Title)},
                  description AS {nameof(InitiativeResponse.Description)},
                  category_id AS {nameof(InitiativeResponse.CategoryId)}
              FROM initiatives.initiatives
              WHERE id = @InitiativeId
              """;

        InitiativeResponse? initiative = await connection.QuerySingleOrDefaultAsync<InitiativeResponse>(sql, request);

        return initiative;
    }
}
