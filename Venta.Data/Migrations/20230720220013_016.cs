using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Venta.Data.Migrations
{
    /// <inheritdoc />
    public partial class _016 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalesClothing");

            migrationBuilder.CreateTable(
                name: "SalesClothingSize",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalesId = table.Column<int>(type: "int", nullable: false),
                    ClothingId = table.Column<int>(type: "int", nullable: false),
                    ClothingSizeId = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    PriceUnit = table.Column<decimal>(type: "decimal(8,2)", precision: 8, scale: 2, nullable: false),
                    CreateBy = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesClothingSize", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesClothingSize_ClothingSize_ClothingSizeId",
                        column: x => x.ClothingSizeId,
                        principalTable: "ClothingSize",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SalesClothingSize_Clothing_ClothingId",
                        column: x => x.ClothingId,
                        principalTable: "Clothing",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SalesClothingSize_Sales_SalesId",
                        column: x => x.SalesId,
                        principalTable: "Sales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SalesClothingSize_ClothingId",
                table: "SalesClothingSize",
                column: "ClothingId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesClothingSize_ClothingSizeId",
                table: "SalesClothingSize",
                column: "ClothingSizeId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesClothingSize_SalesId",
                table: "SalesClothingSize",
                column: "SalesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalesClothingSize");

            migrationBuilder.CreateTable(
                name: "SalesClothing",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClothingId = table.Column<int>(type: "int", nullable: false),
                    SalesId = table.Column<int>(type: "int", nullable: false),
                    CreateBy = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    PriceUnit = table.Column<decimal>(type: "decimal(8,2)", precision: 8, scale: 2, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesClothing", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesClothing_Clothing_ClothingId",
                        column: x => x.ClothingId,
                        principalTable: "Clothing",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SalesClothing_Sales_SalesId",
                        column: x => x.SalesId,
                        principalTable: "Sales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SalesClothing_ClothingId",
                table: "SalesClothing",
                column: "ClothingId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesClothing_SalesId",
                table: "SalesClothing",
                column: "SalesId");
        }
    }
}
