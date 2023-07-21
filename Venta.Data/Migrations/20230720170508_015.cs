using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Venta.Data.Migrations
{
    /// <inheritdoc />
    public partial class _015 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PurchaseId",
                table: "Activity",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Activity_PurchaseId",
                table: "Activity",
                column: "PurchaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activity_Purchase_PurchaseId",
                table: "Activity",
                column: "PurchaseId",
                principalTable: "Purchase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activity_Purchase_PurchaseId",
                table: "Activity");

            migrationBuilder.DropIndex(
                name: "IX_Activity_PurchaseId",
                table: "Activity");

            migrationBuilder.DropColumn(
                name: "PurchaseId",
                table: "Activity");
        }
    }
}
