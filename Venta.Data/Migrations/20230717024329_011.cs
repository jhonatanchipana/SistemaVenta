using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Venta.Data.Migrations
{
    /// <inheritdoc />
    public partial class _011 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Size",
                table: "Clothing");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Size",
                table: "Clothing",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
