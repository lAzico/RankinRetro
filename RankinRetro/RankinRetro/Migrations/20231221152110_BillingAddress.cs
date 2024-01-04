using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RankinRetro.Migrations
{
    /// <inheritdoc />
    public partial class BillingAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "OrderAddresses",
                newName: "ShippingAddressId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShippingAddressId",
                table: "OrderAddresses",
                newName: "Id");
        }
    }
}
