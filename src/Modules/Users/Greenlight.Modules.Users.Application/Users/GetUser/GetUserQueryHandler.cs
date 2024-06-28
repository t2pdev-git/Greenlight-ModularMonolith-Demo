using System.Data.Common;
using Dapper;
using Greenlight.Common.Application.Data;
using Greenlight.Common.Application.Messaging;
using Greenlight.Common.Domain;
using Greenlight.Modules.Users.Domain.Users;

namespace Greenlight.Modules.Users.Application.Users.GetUser;

internal sealed class GetUserQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetUserQuery, UserResponse>
{
    public async Task<Result<UserResponse>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();
        // :DLO:1 Replace Dapper with EF Core query. See: Will This New EF Core Feature Be The End Of Dapper? https://www.youtube.com/watch?v=Egd-BMPCHNc 
        // Also review "EF Core Performance Optimization Challenge | 233x FASTER" https://www.youtube.com/watch?v=jSiGyPHqnpY
        const string sql =
            $"""
             SELECT
                 id AS {nameof(UserResponse.Id)},
                 email AS {nameof(UserResponse.Email)},
                 first_name AS {nameof(UserResponse.FirstName)},
                 last_name AS {nameof(UserResponse.LastName)}
             FROM users.users
             WHERE id = @UserId
             """;

        UserResponse? user = await connection.QuerySingleOrDefaultAsync<UserResponse>(sql, request);

        if (user is null)
        {
            return Result.Failure<UserResponse>(UserErrors.NotFound(request.UserId));
        }

        return user;
    }
}
