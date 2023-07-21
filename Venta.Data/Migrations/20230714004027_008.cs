using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Venta.Data.Migrations
{
    /// <inheritdoc />
    public partial class _008 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvestmentUnit",
                table: "SalesClothing");

            migrationBuilder.RenameColumn(
                name: "PriceSold",
                table: "SalesClothing",
                newName: "PriceUnit");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PriceUnit",
                table: "SalesClothing",
                newName: "PriceSold");

            migrationBuilder.AddColumn<decimal>(
                name: "InvestmentUnit",
                table: "SalesClothing",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }
    }
}
