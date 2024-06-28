using System.Data.Common;
using Greenlight.Common.Application.Data;
using Npgsql;

namespace Greenlight.Common.Infrastructure.Data;
internal sealed class DbConnectionFactory(NpgsqlDataSource datasource) : IDbConnectionFactory
{
    public async ValueTask<DbConnection> OpenConnectionAsync()
    {
        return await datasource.OpenConnectionAsync();
    }
}
