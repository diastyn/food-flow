namespace FoodFlow.BuildingBlocks.Authorization;

public static class AppPermissions
{
    public static class Users
    {
        public const string Read = "users:read";
        public const string Write = "users:write";
        public const string Delete = "users:delete";
        public const string ManageRoles = "users:manage-roles";
    }

    public static class Roles
    {
        public const string Read = "roles:read";
        public const string Write = "roles:write";
        public const string Delete = "roles:delete";
        public const string ManagePermissions = "roles:manage-permissions";
    }

    public static class Orders
    {
        public const string Read = "orders:read";
        public const string Write = "orders:write";
        public const string Delete = "orders:delete";
    }

    public static string[] GetAllIdentityPermissions()
    {
        return
        [
            Users.Read,
            Users.Write,
            Users.Delete,
            Users.ManageRoles,
            Roles.Read,
            Roles.Write,
            Roles.Delete,
            Roles.ManagePermissions
        ];
    }

    public static string[] GetAll() => [.. GetAllIdentityPermissions(), Orders.Read, Orders.Write, Orders.Delete];
}
