﻿using Greenlight.Modules.Users.Domain.Users;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Greenlight.Modules.Users.Infrastructure.Users;

internal sealed class RoleDbConfig : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("roles");

        builder.HasKey(r => r.Name);

        builder.Property(r => r.Name).HasMaxLength(50);

        builder
            .HasMany<User>()
            .WithMany(u => u.Roles)
            .UsingEntity(joinBuilder =>
            {
                joinBuilder.ToTable("user_roles");

                joinBuilder.Property("RolesName").HasColumnName("role_name");
            });

        builder.HasData(
            Role.Member,
            Role.Administrator);
    }
}
