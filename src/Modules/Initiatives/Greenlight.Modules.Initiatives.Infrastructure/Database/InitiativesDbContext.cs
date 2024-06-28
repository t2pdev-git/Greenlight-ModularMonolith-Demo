using System.Reflection;
using Greenlight.Common.Infrastructure.Inbox;
using Greenlight.Common.Infrastructure.Outbox;
using Greenlight.Modules.Initiatives.Application.Abstractions.Data;
using Greenlight.Modules.Initiatives.Domain.Categories;
using Greenlight.Modules.Initiatives.Domain.Initiatives;
using Greenlight.Modules.Initiatives.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Greenlight.Modules.Initiatives.Infrastructure.Database;

public sealed class InitiativesDbContext(DbContextOptions<InitiativesDbContext> options)
    : DbContext(options), IUnitOfWork
{
    internal DbSet<Category> Categories { get; set; }
    internal DbSet<Initiative> Initiatives { get; set; }
    internal DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Initiatives);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.ApplyConfiguration(new InboxMessageDbConfig());
        modelBuilder.ApplyConfiguration(new InboxMessageConsumerDbConfig());

        modelBuilder.ApplyConfiguration(new OutboxMessageDbConfig());
        modelBuilder.ApplyConfiguration(new OutboxMessageConsumerDbConfig());
    }

    protected override void ConfigureConventions(
        ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<decimal>()
            .HavePrecision(18, 6);
    }
}
