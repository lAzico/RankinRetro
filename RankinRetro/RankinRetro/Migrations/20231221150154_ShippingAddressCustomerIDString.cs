using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RankinRetro.Migrations
{
    /// <inheritdoc />
    public partial class ShippingAddressCustomerIDString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CustomerId",
                table: "OrderAddresses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "OrderAddresses",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
