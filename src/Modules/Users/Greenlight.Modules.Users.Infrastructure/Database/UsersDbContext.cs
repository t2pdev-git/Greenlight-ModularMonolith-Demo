using System.Reflection;
using Greenlight.Common.Infrastructure.Inbox;
using Greenlight.Common.Infrastructure.Outbox;
using Greenlight.Modules.Users.Application.Abstractions.Data;
using Greenlight.Modules.Users.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Greenlight.Modules.Users.Infrastructure.Database;

public sealed class UsersDbContext(DbContextOptions<UsersDbContext> options) 
    : DbContext(options), IUnitOfWork
{
    internal DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Users);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.ApplyConfiguration(new InboxMessageDbConfig());
        modelBuilder.ApplyConfiguration(new InboxMessageConsumerDbConfig());

        modelBuilder.ApplyConfiguration(new OutboxMessageDbConfig());
        modelBuilder.ApplyConfiguration(new OutboxMessageConsumerDbConfig());
    }
}
