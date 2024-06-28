namespace Greenlight.Modules.Users.Domain.Users;

public sealed class Permission(string code)
{
    // Categories
    public static readonly Permission GetCategories = new("categories:read");
    public static readonly Permission ModifyCategories = new("categories:update");

    //Initiatives
    public static readonly Permission GetInitiatives = new("initiatives:read");
    public static readonly Permission SearchInitiatives = new("initiatives:search");
    public static readonly Permission ModifyInitiatives = new("initiatives:update");

    // Users
    public static readonly Permission GetUser = new("users:read");
    public static readonly Permission ModifyUser = new("users:update");

    public string Code { get; } = code;
}
