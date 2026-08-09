using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodFlow.Modules.Ordering.Infrastructure.Persistence.Migrations;

/// <inheritdoc />
public partial class Initial : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        _ = migrationBuilder.EnsureSchema(
            name: "ordering");

        _ = migrationBuilder.CreateTable(
            name: "Orders",
            schema: "ordering",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                RestaurantId = table.Column<Guid>(type: "uuid", nullable: false),
                CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                Status = table.Column<int>(type: "integer", nullable: false),
                DeliveryAddressCity = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                DeliveryAddressCountry = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                DeliveryAddressPostalCode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                DeliveryAddressStreet = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                TotalPriceAmount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                TotalPriceCurrency = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false)
            },
            constraints: table =>
            {
                _ = table.PrimaryKey("PK_Orders", x => x.Id);
            });

        _ = migrationBuilder.CreateTable(
            name: "OrderItems",
            schema: "ordering",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                UnitPrice = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                Quantity = table.Column<int>(type: "integer", nullable: false),
                ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                ProductName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
            },
            constraints: table =>
            {
                _ = table.PrimaryKey("PK_OrderItems", x => x.Id);
                _ = table.ForeignKey(
                    name: "FK_OrderItems_Orders_OrderId",
                    column: x => x.OrderId,
                    principalSchema: "ordering",
                    principalTable: "Orders",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        _ = migrationBuilder.CreateIndex(
            name: "IX_OrderItems_OrderId",
            schema: "ordering",
            table: "OrderItems",
            column: "OrderId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        _ = migrationBuilder.DropTable(
            name: "OrderItems",
            schema: "ordering");

        _ = migrationBuilder.DropTable(
            name: "Orders",
            schema: "ordering");
    }
}
