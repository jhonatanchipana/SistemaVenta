using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Venta.Data.Migrations
{
    /// <inheritdoc />
    public partial class _003 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClothingManufacturing_Clothing_ClothingId",
                table: "ClothingManufacturing");

            migrationBuilder.DropIndex(
                name: "IX_ClothingManufacturing_ClothingId",
                table: "ClothingManufacturing");

            migrationBuilder.DropColumn(
                name: "ClothingId",
                table: "ClothingManufacturing");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "ClothingManufacturing",
                newName: "QuantityTotal");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "QuantityTotal",
                table: "ClothingManufacturing",
                newName: "Quantity");

            migrationBuilder.AddColumn<int>(
                name: "ClothingId",
                table: "ClothingManufacturing",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ClothingManufacturing_ClothingId",
                table: "ClothingManufacturing",
                column: "ClothingId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClothingManufacturing_Clothing_ClothingId",
                table: "ClothingManufacturing",
                column: "ClothingId",
                principalTable: "Clothing",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
