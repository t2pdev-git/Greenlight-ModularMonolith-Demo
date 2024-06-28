using Greenlight.Modules.Users.Domain.Users;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Greenlight.Modules.Users.Infrastructure.Users;

internal sealed class PermissionDbConfig : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("permissions");

        builder.HasKey(p => p.Code);

        builder.Property(p => p.Code)
            .HasMaxLength(100);

        builder.HasData(
            Permission.GetCategories,
            Permission.ModifyCategories,

            Permission.GetInitiatives,
            Permission.ModifyInitiatives,
            Permission.SearchInitiatives,

            Permission.GetUser,
            Permission.ModifyUser);

        builder
            .HasMany<Role>()
            .WithMany()
            .UsingEntity(joinBuilder =>
            {
                joinBuilder.ToTable("role_permissions");
        
                joinBuilder.HasData(
                    // Member permissions
                    CreateRolePermission(Role.Member, Permission.GetCategories),
                    CreateRolePermission(Role.Member, Permission.ModifyCategories),
        
                    CreateRolePermission(Role.Member, Permission.GetInitiatives),
                    CreateRolePermission(Role.Member, Permission.ModifyInitiatives),
                    CreateRolePermission(Role.Member, Permission.SearchInitiatives),
        
                    CreateRolePermission(Role.Member, Permission.GetUser),
                    CreateRolePermission(Role.Member, Permission.ModifyUser),
        
                    // Admin permissions
                    CreateRolePermission(Role.Administrator, Permission.GetCategories),
                    CreateRolePermission(Role.Administrator, Permission.ModifyCategories),
        
                    CreateRolePermission(Role.Administrator, Permission.GetInitiatives),
                    CreateRolePermission(Role.Administrator, Permission.ModifyInitiatives),
                    CreateRolePermission(Role.Administrator, Permission.SearchInitiatives),
        
                    CreateRolePermission(Role.Administrator, Permission.GetUser),
                    CreateRolePermission(Role.Administrator, Permission.ModifyUser));
            });
    }

    private static object CreateRolePermission(Role role, Permission permission)
    {
        return new
        {
            RoleName = role.Name,
            PermissionCode = permission.Code
        };
    }
}
