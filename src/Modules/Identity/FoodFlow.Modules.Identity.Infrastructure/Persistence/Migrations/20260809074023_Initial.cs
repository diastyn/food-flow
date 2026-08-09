using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodFlow.Modules.Identity.Infrastructure.Persistence.Migrations;

/// <inheritdoc />
public partial class Initial : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        _ = migrationBuilder.EnsureSchema(
            name: "identity");

        _ = migrationBuilder.CreateTable(
            name: "Permissions",
            schema: "identity",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                Description = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true)
            },
            constraints: table =>
            {
                _ = table.PrimaryKey("PK_Permissions", x => x.Id);
            });

        _ = migrationBuilder.CreateTable(
            name: "Roles",
            schema: "identity",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                Description = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true)
            },
            constraints: table =>
            {
                _ = table.PrimaryKey("PK_Roles", x => x.Id);
            });

        _ = migrationBuilder.CreateTable(
            name: "Users",
            schema: "identity",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Username = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                Email = table.Column<string>(type: "character varying(320)", maxLength: 320, nullable: false),
                PasswordHash = table.Column<string>(type: "text", nullable: false),
                Phone = table.Column<string>(type: "text", nullable: true),
                IsActive = table.Column<bool>(type: "boolean", nullable: false),
                CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                LastLoginAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                LastFailedLoginAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                FailedLoginAttempts = table.Column<int>(type: "integer", nullable: false),
                LockedUntil = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                EmailVerified = table.Column<bool>(type: "boolean", nullable: false),
                EmailVerifiedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                FullName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                LastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
            },
            constraints: table =>
            {
                _ = table.PrimaryKey("PK_Users", x => x.Id);
            });

        _ = migrationBuilder.CreateTable(
            name: "RolePermissions",
            schema: "identity",
            columns: table => new
            {
                PermissionsId = table.Column<Guid>(type: "uuid", nullable: false),
                RoleId = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table =>
            {
                _ = table.PrimaryKey("PK_RolePermissions", x => new { x.PermissionsId, x.RoleId });
                _ = table.ForeignKey(
                    name: "FK_RolePermissions_Permissions_PermissionsId",
                    column: x => x.PermissionsId,
                    principalSchema: "identity",
                    principalTable: "Permissions",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                _ = table.ForeignKey(
                    name: "FK_RolePermissions_Roles_RoleId",
                    column: x => x.RoleId,
                    principalSchema: "identity",
                    principalTable: "Roles",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        _ = migrationBuilder.CreateTable(
            name: "UserRoles",
            schema: "identity",
            columns: table => new
            {
                RolesId = table.Column<Guid>(type: "uuid", nullable: false),
                UserId = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table =>
            {
                _ = table.PrimaryKey("PK_UserRoles", x => new { x.RolesId, x.UserId });
                _ = table.ForeignKey(
                    name: "FK_UserRoles_Roles_RolesId",
                    column: x => x.RolesId,
                    principalSchema: "identity",
                    principalTable: "Roles",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                _ = table.ForeignKey(
                    name: "FK_UserRoles_Users_UserId",
                    column: x => x.UserId,
                    principalSchema: "identity",
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        _ = migrationBuilder.CreateIndex(
            name: "IX_Permissions_Name",
            schema: "identity",
            table: "Permissions",
            column: "Name",
            unique: true);

        _ = migrationBuilder.CreateIndex(
            name: "IX_RolePermissions_RoleId",
            schema: "identity",
            table: "RolePermissions",
            column: "RoleId");

        _ = migrationBuilder.CreateIndex(
            name: "IX_Roles_Name",
            schema: "identity",
            table: "Roles",
            column: "Name",
            unique: true);

        _ = migrationBuilder.CreateIndex(
            name: "IX_UserRoles_UserId",
            schema: "identity",
            table: "UserRoles",
            column: "UserId");

        _ = migrationBuilder.CreateIndex(
            name: "IX_Users_Email",
            schema: "identity",
            table: "Users",
            column: "Email",
            unique: true);

        _ = migrationBuilder.CreateIndex(
            name: "IX_Users_Username",
            schema: "identity",
            table: "Users",
            column: "Username",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        _ = migrationBuilder.DropTable(
            name: "RolePermissions",
            schema: "identity");

        _ = migrationBuilder.DropTable(
            name: "UserRoles",
            schema: "identity");

        _ = migrationBuilder.DropTable(
            name: "Permissions",
            schema: "identity");

        _ = migrationBuilder.DropTable(
            name: "Roles",
            schema: "identity");

        _ = migrationBuilder.DropTable(
            name: "Users",
            schema: "identity");
    }
}
