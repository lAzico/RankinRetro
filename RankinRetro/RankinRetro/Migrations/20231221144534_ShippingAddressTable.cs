using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RankinRetro.Migrations
{
    /// <inheritdoc />
    public partial class ShippingAddressTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderAddresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    ShippingFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShippingSurname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShippingTitle = table.Column<int>(type: "int", nullable: false),
                    ShippingAddressFirstline = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShippingAddressSecondline = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShippingCityTown = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShippingAddressPostcode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderAddresses", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderAddresses");
        }
    }
}
